using Application.DTO;
using Application.DTO.Reservations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Reservations
{
    public interface IGetReservationsQuery : IQuery<PagedResponse<ReservationDTO>, SearchReservation>
    {
    }
}
