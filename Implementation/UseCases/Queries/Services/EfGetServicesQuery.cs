using Application.DTO;
using Application.DTO.Beds;
using Application.DTO.Services;
using Application.UseCases.Queries.Services;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Services
{
    public class EfGetServicesQuery : EfUseCase, IGetServicesQuery
    {
        private readonly IMapper _mapper;
        public EfGetServicesQuery(HotelHorizonContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 17;

        public string Name => "Search services";

        public PagedResponse<ServiceDTO> Execute(SearchService search)
        {
            IQueryable<Service> query = Context.Services.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }
            return query.Paged<ServiceDTO, Service>(search, _mapper);
        }
    }
}
