using Application.DTO;
using Application.DTO.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Comments
{
    public interface IGetCommentsQuery : IQuery<PagedResponse<CommentDTO>, SearchComment>
    {
    }
}
