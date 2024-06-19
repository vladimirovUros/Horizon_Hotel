using Application;
using Application.DTO.Reservations;
using Application.Exceptions;
using Application.UseCases.Commands.Reservations;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Reservations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Reservations
{
    public class EfCreateReservationCommand : EfUseCase, ICreateReservationCommand
    {
        private readonly CreateReservationDtoValidator _validator;
        private readonly IApplicationActor _actor;
        public EfCreateReservationCommand(HotelHorizonContext context, CreateReservationDtoValidator validator, IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 35;

        public string Name => "Create reservation";

        public void Execute(CreateReservationDTO data)
        {
            _validator.ValidateAndThrow(data);

            bool roomIsNotAvailable = Context.OccupiedRooms.Any(o => o.RoomId == data.RoomId && (o.Date == data.CheckIn || o.Date == data.CheckOut));
            if (roomIsNotAvailable) {
                throw new ConflictException("Room is not available for selected dates.");
            }
            Room room = Context.Rooms.Include(x => x.Prices).FirstOrDefault(x => x.Id == data.RoomId && x.IsActive);
            if (room == null)
            {
                throw new EntityNotFoundException(nameof(Room), data.RoomId);
            }
            Price activePrice = room.Prices
                .Where(p => p.DateFrom <= DateTime.Now && (p.DateTo == null || p.DateTo >= DateTime.Now) && p.IsActive)
                .FirstOrDefault();
            Reservation reservation = new()
            {
                PhoneNumber = data.PhoneNumber,
                NoOfPeople = data.NumberOfPersons,
                FullName = data.FullName,
                CheckIn = data.CheckIn,
                CheckOut = data.CheckOut,
                RoomId = data.RoomId,
                UserId = _actor.Id,
                Price = activePrice.RoomPrice
            };

            Context.Reservations.Add(reservation);

            List<OccupiedRoom> occupiedRooms = new List<OccupiedRoom>();
            for (DateTime date = data.CheckIn; date <= data.CheckOut; date = date.AddDays(1))
            {
                occupiedRooms.Add(new OccupiedRoom
                {
                    RoomId = data.RoomId,
                    Date = date
                });
            };
            Context.OccupiedRooms.AddRange(occupiedRooms);

            Context.SaveChanges();
        }
    }
}
