using Application.DTO.Reservations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Reservations
{
    public interface IUpdateReservationCommand : ICommand<UpdateReservationDTO>
    {
    }
}
