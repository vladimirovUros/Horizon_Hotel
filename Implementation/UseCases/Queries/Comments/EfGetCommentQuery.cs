using Application.DTO.Comments;
using Application.UseCases.Queries.Comments;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Comments
{
    public class EfGetCommentQuery : EfGenericFindUseCase<CommentDTO, Comment>, IGetCommentQuery
    {
        public EfGetCommentQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public override int Id => 30;

        public override string Name => "Get comment";

        protected override IQueryable<Comment> IncludeRelatedEntities(IQueryable<Comment> query)
        {
            return query.Include(x => x.Author).ThenInclude(x => x.Comments);
        }
    }
}
