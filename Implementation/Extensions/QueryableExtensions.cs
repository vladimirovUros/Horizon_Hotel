using Application.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Extensions
{
    public static class QueryableExtensions
    {
        public static PagedResponse<TDto> Paged<TDto, TEntity>(
            this IQueryable<TEntity> query, PagedSearch search, IMapper mapper)
            where TDto : class
            where TEntity : class
        {
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            IQueryable<TEntity> pagedQuery = query.Skip(skip).Take(perPage);

            return new PagedResponse<TDto>
            {
                CurrentPage = page,
                PerPage = perPage,
                TotalCount = totalCount,
                Data = mapper.ProjectTo<TDto>(pagedQuery).ToList()
            };
        }
    }

}
