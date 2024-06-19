using Application.DTO.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Comments
{
    public interface ICreateCommentCommand : ICommand<CreateCommentDTO>
    {
    }
}
