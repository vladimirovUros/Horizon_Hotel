using Application.DTO.Services;
using Application.Exceptions;
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
    public class EfUpdateServiceCommand : EfUseCase, IUpdateServiceCommand
    {
        private UpdateServiceDtoValidator _validator;
        public EfUpdateServiceCommand(HotelHorizonContext context, UpdateServiceDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 14;

        public string Name => "Update service";

        public void Execute(UpdateServiceDTO data)
        {
            _validator.ValidateAndThrow(data);

            Service service = Context.Services.Find(data.Id) ?? throw new EntityNotFoundException(nameof(Service), data.Id);

            service.Name = data.Name;
            
            if(!string.IsNullOrEmpty(data.IconPath))
                {
                var tempFile = Path.Combine("wwwroot", "temp", data.IconPath);
                var destinactionFile = Path.Combine("wwwroot", "images", "services", data.IconPath);
                File.Move(tempFile, destinactionFile);

                Image oldImage = Context.Images.Find(service.IconId);
                Context.Images.Remove(oldImage);

                service.Icon = new Image
                {
                    Path = $"/images/services/{data.IconPath}"
                };
            };

            Context.SaveChanges();
        }
    }
}
