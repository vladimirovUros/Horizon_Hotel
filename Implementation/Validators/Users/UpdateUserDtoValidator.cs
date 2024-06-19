using Application.DTO.Users;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDTO>
    {
        private readonly HotelHorizonContext _conntext;
        public UpdateUserDtoValidator(HotelHorizonContext context)
        {
            _conntext = context;

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MinimumLength(2)
                .WithMessage("First name must have at least 2 characters.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MinimumLength(3)
                .WithMessage("Last name must have at least 3 characters.");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3)
                .WithMessage("Username must have at least 3 characters.")
                .Must((dto, username) => !_conntext.Users.Any(x => x.Username == username && x.Id != dto.Id))
                .WithMessage("Username is already taken.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email is not in the correct format.")
                .Must((dto, email) => !_conntext.Users.Any(x => x.Email == email && x.Id != dto.Id))
                .WithMessage("Email is already taken.");
        }
    }
}
