using Application;
using Application.DTO.Types;
using Application.UseCases.Commands.Types;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Types
{
    public class EfCreateTypeCommand : EfUseCase, ICreateTypeCommand
    {
        private CreateTypeDtoValidator _validator;
        public EfCreateTypeCommand(HotelHorizonContext context, CreateTypeDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 10;

        public string Name => "Create type";

        public void Execute(CreateTypeDTO data)
        {
            _validator.ValidateAndThrow(data);

            Domain.Type type = new()
            {
                Name = data.Name,
                Capacity = data.Capacity
            };
            Context.Types.Add(type);

            Context.SaveChanges();
        }
    }
}
