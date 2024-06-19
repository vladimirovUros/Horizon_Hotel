using Application.Exceptions;
using Application.UseCases.Commands.Beds;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Beds
{
    public class EfDeleteBedCommand : EfGenericDeleteCommand<Bed>, IDeleteBedCommand
    {
        public EfDeleteBedCommand(HotelHorizonContext context) 
            : base(context)
        {
        }

        public override int Id => 8;

        public override string Name => "Delete bed";
    }
}
