using Application.DTO.Beds;
using Application.UseCases.Queries.Beds;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Beds
{
    public class EfGetBedQuery : EfGenericFindUseCase<BedDTO, Bed>, IGetBedQuery
    {
        public EfGetBedQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public override int Id => 24;

        public override string Name => "Get bed";

        protected override IQueryable<Bed> IncludeRelatedEntities(IQueryable<Bed> query)
        {
            return query.Include(b => b.RoomBeds).ThenInclude(rb => rb.Room);
        }
    }
}
