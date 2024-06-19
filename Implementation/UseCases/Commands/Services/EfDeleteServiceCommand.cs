using Application.Exceptions;
using Application.UseCases.Commands.Services;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Services
{
    public class EfDeleteServiceCommand : EfGenericDeleteCommand<Service>, IDeleteServiceCommand
    {
        public EfDeleteServiceCommand(HotelHorizonContext context) 
            : base(context)
        {
        }

        public override int Id => 15;

        public override string Name => "Delete service";
    }
}
