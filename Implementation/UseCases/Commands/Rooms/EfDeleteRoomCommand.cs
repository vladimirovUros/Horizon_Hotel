using Application.DTO.Rooms;
using Application.Exceptions;
using Application.UseCases.Commands.Rooms;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Rooms
{
    public class EfDeleteRoomCommand : EfGenericDeleteCommand<Room>, IDeleteRoomCommand
    {
        public EfDeleteRoomCommand(HotelHorizonContext context) 
            : base(context)
        {
        }
        public override int Id => 20;

        public override string Name => "Delete room";
    }
}
