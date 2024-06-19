using Application.DTO.Types;
using Application.Exceptions;
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
using Type = Domain.Type;

namespace Implementation.UseCases.Commands.Types
{
    public class EfUpdateTypeCommand : EfUseCase, IUpdateTypeCommand
    {
        private UpdateTypeDtoValidator _validator;
        public EfUpdateTypeCommand(HotelHorizonContext context, UpdateTypeDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 11;

        public string Name => "Update type";

        public void Execute(UpdateTypeDTO data)
        {
            _validator.ValidateAndThrow(data);

            Type type = Context.Types.Find(data.Id) ?? throw new EntityNotFoundException(nameof(Type), data.Id);
            
            type.Name = data.Name;
            type.Capacity = data.Capacity;

            Context.SaveChanges();
        }
    }
}
