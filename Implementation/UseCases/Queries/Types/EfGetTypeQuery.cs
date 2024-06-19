using Application.DTO.Types;
using Application.UseCases.Queries.Types;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Domain.Type;

namespace Implementation.UseCases.Queries.Types
{
    public class EfGetTypeQuery : EfGenericFindUseCase<TypeDTO, Type>, IGetTypeQuery
    {
        public EfGetTypeQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public override int Id => 26;

        public override string Name => "Get type";

        protected override IQueryable<Type> IncludeRelatedEntities(IQueryable<Type> query)
        {
            return query;
        }
    }
}
