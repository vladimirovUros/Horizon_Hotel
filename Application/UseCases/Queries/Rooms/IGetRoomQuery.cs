using Application.DTO.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Rooms
{
    public interface IGetRoomQuery : IQuery<RoomDTO, int>
    {
    }
}
