using Application;
using Application.DTO.Reservations;
using Application.Exceptions;
using Application.UseCases.Commands.Reservations;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Reservations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Reservations
{
    public class EfUpdateReservationCommand : EfUseCase, IUpdateReservationCommand
    {
        private readonly UpdateReservationDtoValidator _validator;
        private readonly IApplicationActor _actor;
        public EfUpdateReservationCommand(HotelHorizonContext context, UpdateReservationDtoValidator validator, IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 36;

        public string Name => "Update reservation";

        public void Execute(UpdateReservationDTO data)
        {
            _validator.ValidateAndThrow(data);

            Reservation reservation = Context.Reservations
                .Include(r => r.Room)
                .ThenInclude(r => r.Prices)
                .FirstOrDefault(r => r.Id == data.ReservationId && r.UserId == _actor.Id);

            if (reservation == null)
            {
                throw new EntityNotFoundException(nameof(Reservation), data.ReservationId);
            }

            TimeSpan timeUntilCheckIn = reservation.CheckIn - DateTime.Now;
            if (timeUntilCheckIn.TotalHours < 48)
            {
                throw new ConflictException("You can't update reservation less than 48 hours before check in.");
            }


            DateTime checkIn = reservation.CheckIn;
            DateTime checkOut = reservation.CheckOut;

            //bool roomIsNotAvailable = Context.OccupiedRooms.Any(o => o.RoomId == reservation.RoomId && (o.Date == data.CheckIn || o.Date == data.CheckOut));
            bool roomIsNotAvailable = Context.OccupiedRooms.Any(o => o.RoomId == data.RoomId && o.Date >= data.CheckIn && o.Date <= data.CheckOut && o.Id != data.ReservationId);

            if(roomIsNotAvailable)
            {
                throw new ConflictException("Room is not available for selected dates.");
            }
            if (reservation.Room == null || !reservation.Room.IsActive)
            {
                throw new EntityNotFoundException(nameof(Room), data.RoomId);
            }

            Price activePrice = reservation.Room.Prices
                .Where(p => p.DateFrom <= DateTime.Now && (p.DateTo == null || p.DateTo >= DateTime.Now) && p.IsActive)
                .FirstOrDefault();


            if (reservation.UserId != _actor.Id)
            {
                throw new UnauthorizedAccessException();
            }

            reservation.CheckIn = data.CheckIn;
            reservation.CheckOut = data.CheckOut;
            reservation.FullName = data.FullName;
            reservation.NoOfPeople = data.NumberOfPersons;
            reservation.PhoneNumber = data.PhoneNumber;
            reservation.Price = activePrice.RoomPrice;
            reservation.RoomId = data.RoomId;

            List<OccupiedRoom> oldOccupiedRooms = Context.OccupiedRooms.Where(o => o.RoomId == data.RoomId && o.Date >= checkIn && o.Date <= checkOut).ToList();
            
            Context.OccupiedRooms.RemoveRange(oldOccupiedRooms);

            List<OccupiedRoom> newOccupiedRooms = new List<OccupiedRoom>();
            for (DateTime date = data.CheckIn; date <= data.CheckOut; date = date.AddDays(1))
            {
                newOccupiedRooms.Add(new OccupiedRoom
                {
                    RoomId = data.RoomId,
                    Date = date,
                });
            };
            Context.OccupiedRooms.AddRange(newOccupiedRooms);

            Context.SaveChanges();
        }
    }
}
