using Application.DTO.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Rooms
{
    public interface ICreateRoomCommand : ICommand<CreateRoomDTO>
    {
    }
}
