using Application.DTO.Beds;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Beds
{
    public class CreateBedDtoValidator : AbstractValidator<CreateBedDTO>
    {
        private readonly HotelHorizonContext _context;
        public CreateBedDtoValidator(HotelHorizonContext context)
        {
            _context = context;
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Bed name must have at least 3 characters.")
                .Must(name => !_context.Beds.Any(bed => bed.Name == name))
                .WithMessage("Bed with this name already exists, try something else please");

        }
    }
}
