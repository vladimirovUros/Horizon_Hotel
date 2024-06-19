using Application.DTO.Beds;
using Application.Exceptions;
using Application.UseCases.Commands.Beds;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Beds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Beds
{
    public class EfUpdateBedCommand : EfUseCase, IUpdateBedCommand
    {
        private UpdateBedDtoValidator _validator;
        public EfUpdateBedCommand(HotelHorizonContext context, UpdateBedDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Update bed";

        public void Execute(UpdateBedDTO data)
        {
            _validator.ValidateAndThrow(data);

            Bed bed = Context.Beds.Find(data.Id) ?? throw new EntityNotFoundException(nameof(Bed), data.Id);

            bed.Name = data.Name;

            Context.SaveChanges();
        }
    }
}
