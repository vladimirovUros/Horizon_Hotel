using Application.DTO.Users;
using DataAccess;
using FluentValidation;
using Implementation.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDtoValidator(HotelHorizonContext context)
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()
                .Must(x => !context.Users.Any(u => u.Email == x))
                .WithMessage("Email is already in use.");
            
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("First name must be at least 2 characters long.");
            
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Last name must be at least 3 characters long.");
            
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$")
                .WithMessage("Password requires at least eight characters, at least one uppercase letter, one lowercase letter and one number. Exemple: Something123");
            
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Matches("(?=.{4,15}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Invalid username format. Exemple: Djokovic123")
                .Must(x => !context.Users.Any(u => u.Username == x))
                .WithMessage("Username is already in use.");

            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => (DateTime.UtcNow - x.Value).TotalDays > (14 * 365))
                .WithMessage("You have to be at least 14 years old.");
        }
    }
}
