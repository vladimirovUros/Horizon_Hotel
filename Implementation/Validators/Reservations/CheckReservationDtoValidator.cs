using Application.DTO.Reservations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Reservations
{
    public class CheckReservationDtoValidator : AbstractValidator<CheckReservationDTO>
    {
        public CheckReservationDtoValidator()
        {
            RuleFor(x => x.CheckIn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Reservation date is required.")
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Reservation date cant be in the past");

            RuleFor(x => x.CheckOut).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Check out date is required.")
                .Must((dto, checkOut) => (checkOut - dto.CheckIn).TotalDays >= 1)
                .WithMessage("Reservation must be at least 1 day long.");
        }
    }
}
