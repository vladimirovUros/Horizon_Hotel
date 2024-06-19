using Application.DTO.Services;
using Application.UseCases.Queries.Beds;
using Application.UseCases.Queries.Services;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Services
{
    public class EfGetServiceQuery : EfGenericFindUseCase<ServiceDTO, Service>, IGetServiceQuery
    {
        public EfGetServiceQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public override int Id => 25;

        public override string Name => "Get service";

        protected override IQueryable<Service> IncludeRelatedEntities(IQueryable<Service> query)
        {
            return query.Include(x => x.Icon);
        }
    }
}
