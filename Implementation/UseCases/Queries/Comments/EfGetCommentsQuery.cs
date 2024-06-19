using Application.DTO;
using Application.DTO.Comments;
using Application.UseCases.Queries.Comments;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Comments
{
    public class EfGetCommentsQuery : EfUseCase, IGetCommentsQuery
    {
        private readonly IMapper _mapper;
        public EfGetCommentsQuery(HotelHorizonContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 31;

        public string Name => "Search comments";

        public PagedResponse<CommentDTO> Execute(SearchComment search)
        {
            IQueryable<Comment> query = Context.Comments.Include(x => x.Author)
                                .Where(x => x.Room.IsActive && x.IsActive)
                                .AsQueryable();


            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.Text.ToLower().Contains(search.Keyword.ToLower()));
            }
            if (search.CreatedAt.HasValue)
            {
                query = query.Where(x => x.CreatedAt.Date >= search.CreatedAt);
            }

            return query.Paged<CommentDTO, Comment>(search, _mapper);
        }
    }
}
