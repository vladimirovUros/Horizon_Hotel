using Application.DTO;
using Application.DTO.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Rooms
{
    public interface IGetRoomsQuery : IQuery<PagedResponse<RoomDTO>, SearchRoom>
    {
    }
}
