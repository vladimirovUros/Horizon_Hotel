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
    public class CreateServiceDtoValiator : AbstractValidator<CreateServiceDTO>
    {
        public CreateServiceDtoValiator(HotelHorizonContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Service name must have at least 3 characters.")
                .Must(name => !context.Services.Any(service => service.Name == name))
                .WithMessage("Service with this name already exists, try something else please");

            RuleFor(x => x.IconPath)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Icon path is required.")
                .Must((x, fileName) =>
                {
                    string path = Path.Combine("wwwroot", "temp", fileName);

                    return Path.Exists(path);

                }).WithMessage("File doesn't exist.");

        }
    }
}
