using Application.DTO.Types;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Types
{
    public class CreateTypeDtoValidator : AbstractValidator<CreateTypeDTO>
    {
        private readonly HotelHorizonContext _context;
        public CreateTypeDtoValidator(HotelHorizonContext context)
        {
            _context = context;
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Type name must have at least 3 characters.")
                .Must(name => !_context.Types.Any(type => type.Name == name))
                .WithMessage("Type with this name already exists, try something else please");

            RuleFor(x => x.Capacity)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Capacity is required.")
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0.")
                .LessThan(7)
                .WithMessage("Capacity must be less than 7.");
        }
    }
}
