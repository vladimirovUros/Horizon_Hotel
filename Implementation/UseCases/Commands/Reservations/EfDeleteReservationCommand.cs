using Application.UseCases.Commands.Reservations;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Reservations
{
    public class EfDeleteReservationCommand : EfGenericDeleteCommand<Reservation>, IDeleteReservationCommand
    {
        public EfDeleteReservationCommand(HotelHorizonContext context) 
            : base(context)
        {
        }

        public override int Id => 37;

        public override string Name => "Delete reservation";
    }
}
