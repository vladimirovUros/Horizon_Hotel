using Application.DTO.Reservations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Reservations
{
    public interface IGetReservationQuery : IQuery<ReservationDTO, int>
    {
    }
}
