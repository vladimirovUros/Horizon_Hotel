using Application.DTO.Reservations;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Reservations
{
    public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDTO>
    {
        public CreateReservationDtoValidator(HotelHorizonContext context)
        {
            RuleFor(x => x.FullName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Full name is required. Exemple: Novak Djokivc")
                .MaximumLength(50)
                .WithMessage("Full name can't be longer than 50 characters.")
                .MinimumLength(4)
                .WithMessage("Full name must be at least 4 characters long.");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MaximumLength(20)
                .WithMessage("Phone number can't be longer than 20 characters.");

            RuleFor(x => x.CheckIn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Reservation date is required.")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Reservation date cant be in the past");

            RuleFor(x => x.CheckOut).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Check out date is required.")
                .Must((dto, checkOut) => (checkOut - dto.CheckIn).TotalDays >= 1)
                .WithMessage("Reservation must be at least 1 day long.");

            RuleFor(x => x.NumberOfPersons)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Number of people is required.")
                .GreaterThan(0)
                .WithMessage("Number of people must be greater than 0.")
                .Must((dto, numOfPeople) => numOfPeople <= context.Rooms.Where(y => y.Id == dto.RoomId).Select(x => x.Type.Capacity).FirstOrDefault())
                .When(dto => context.Rooms.Any(r => r.Id == dto.RoomId && r.IsActive))
                .WithMessage("Number of people cannot be greater then available capacity for this room");

            RuleFor(x => x.RoomId)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("Room ID is required.")
               .Must(roomId => context.Rooms.Any(room => room.Id == roomId && room.IsActive))
               .WithMessage("Room does not exist.");
        }
    }
 }
