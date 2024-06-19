using Application.DTO;
using Application.DTO.Reservations;
using Application.UseCases.Queries.Reservations;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Reservations
{
    public class EfGetReservationsQuery : EfUseCase, IGetReservationsQuery
    {
        private readonly IMapper _mapper;
        public EfGetReservationsQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 38;

        public string Name => "Search reservations";

        public PagedResponse<ReservationDTO> Execute(SearchReservation search)
        {
            IQueryable<Reservation> query = Context.Reservations.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.User.FirstName.Contains(search.Keyword.ToLower()) ||
                                                            x.User.LastName.Contains(search.Keyword.ToLower()));
            }
            if (search.CheckIn.HasValue)
            {
                query = query.Where(x => x.CheckIn >= search.CheckIn);
            }
            if (search.CheckOut.HasValue)
            {
                query = query.Where(x => x.CheckOut <= search.CheckOut);
            }
            if(search.IsActive)
            {
                query = query.Where(x => x.IsActive == search.IsActive);
            }
            return query.Paged<ReservationDTO, Reservation>(search, _mapper);
        }
    }
}
