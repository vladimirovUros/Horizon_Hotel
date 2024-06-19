using Application.Exceptions;
using Application.UseCases;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.UseCases
{
    public abstract class EfGenericFindUseCase<TResult, TEntity> : EfUseCase, IQuery<TResult, int>
        where TEntity : Entity
        where TResult : class
    {
        private readonly IMapper _mapper;
        protected EfGenericFindUseCase(HotelHorizonContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public abstract int Id { get; }

        public abstract string Name { get; }

        protected abstract IQueryable<TEntity> IncludeRelatedEntities(IQueryable<TEntity> query);

        public TResult Execute(int search)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            query = IncludeRelatedEntities(query);

            TEntity entity = query.FirstOrDefault(x => x.Id == search && x.IsActive) ?? throw new EntityNotFoundException(nameof(TEntity), search);

            return _mapper.Map<TResult>(entity);
        }
    }
}
