using Application.DTO.Beds;
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
    public class EfCreateBedCommand : EfUseCase, ICreateBedCommand
    {
        private CreateBedDtoValidator _validator;
        public EfCreateBedCommand(HotelHorizonContext context, CreateBedDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "Create bed";

        public void Execute(CreateBedDTO data)
        {
            _validator.ValidateAndThrow(data);

            Bed bed = new()
            {
                Name = data.Name
            };

            Context.Beds.Add(bed);

            Context.SaveChanges();
        }
    }
}
