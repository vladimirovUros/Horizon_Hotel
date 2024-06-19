using Application;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfDeleteUserCommand : EfGenericDeleteCommand<User>, IDeleteUserCommand
    {

        public EfDeleteUserCommand(HotelHorizonContext context)
            : base(context)
        {
        }
        public override int Id => 4;

        public override string Name => "Delete user";
    }
}
