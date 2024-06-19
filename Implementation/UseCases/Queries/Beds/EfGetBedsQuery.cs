using Application.DTO;
using Application.DTO.Beds;
using Application.UseCases;
using Application.UseCases.Queries.Beds;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Beds
{
    public class EfGetBedsQuery : EfUseCase, IGetBedsQuery
    {
        private readonly IMapper _mapper;
        public EfGetBedsQuery(HotelHorizonContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 9;

        public string Name => "Search beds";

        public PagedResponse<BedDTO> Execute(SearchBed search)
        {
            IQueryable<Bed> query = Context.Beds.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }
            return query.Paged<BedDTO, Bed>(search, _mapper);
        }
    }
}
