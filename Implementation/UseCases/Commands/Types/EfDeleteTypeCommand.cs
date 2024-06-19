using Application.Exceptions;
using Application.UseCases.Commands.Services;
using Application.UseCases.Commands.Types;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Domain.Type;

namespace Implementation.UseCases.Commands.Types
{
    public class EfDeleteTypeCommand : EfGenericDeleteCommand<Type>, IDeleteTypeCommand
    {
        public EfDeleteTypeCommand(HotelHorizonContext context) 
            : base(context)
        {
        }

        public override int Id => 12;

        public override string Name => "Delete type";
    }
}
