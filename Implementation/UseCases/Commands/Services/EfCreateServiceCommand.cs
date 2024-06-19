using Application.DTO.Services;
using Application.UseCases.Commands.Services;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Services
{
    public class EfCreateServiceCommand : EfUseCase, ICreateServiceCommand
    {
        private CreateServiceDtoValiator _validator;
        public EfCreateServiceCommand(HotelHorizonContext context, CreateServiceDtoValiator validator) 
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Create service";

        public void Execute(CreateServiceDTO data)
        {
            _validator.ValidateAndThrow(data);

            var tempFile = Path.Combine("wwwroot", "temp", data.IconPath);
            var destinactionFile = Path.Combine("wwwroot", "images", "services", data.IconPath);
            File.Move(tempFile, destinactionFile);

            Service service = new()
            {
                Name = data.Name,
                Icon = new Image
                {
                    Path = $"/images/services/{data.IconPath}"
                }
            };

            Context.Services.Add(service);

            Context.SaveChanges();
        }
    }
}
