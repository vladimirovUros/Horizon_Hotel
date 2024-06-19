using Application.DTO;
using Application.DTO.Types;
using Application.UseCases.Queries.Types;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Domain.Type;

namespace Implementation.UseCases.Queries.Types
{
    public class EfGetTypesQuery : EfUseCase, IGetTypesQuery
    {
        private readonly IMapper _mapper;
        public EfGetTypesQuery(HotelHorizonContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 16;

        public string Name => "Search types";

        public PagedResponse<TypeDTO> Execute(SearchType search)
        {
            IQueryable<Type> query = Context.Types.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }
            if (search.MinimumCapacity.HasValue)
            {
                query = query.Where(x => x.Capacity >= search.MinimumCapacity);
            }
            if (search.MaximumCapacity.HasValue)
            {
                query = query.Where(x => x.Capacity <= search.MaximumCapacity);
            }
            return query.Paged<TypeDTO, Type>(search, _mapper);
        }
    }
}
