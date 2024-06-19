using Application.DTO.Services;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Services
{
    public class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDTO>
    {
        public UpdateServiceDtoValidator(HotelHorizonContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Service name must have at least 3 characters.")
                .Must((dto, name) => !context.Services.Any(service => service.Name == name && service.Id != dto.Id))
                .WithMessage("Service with this name already exists, try something else please")
                .Must((dto, name) =>
                {
                    var existingService = context.Services.FirstOrDefault(service => service.Id == dto.Id);
                    return existingService != null && existingService.Name != name;
                })
                .WithMessage("Service name must be different from the existing one.");
        }
    }
}
