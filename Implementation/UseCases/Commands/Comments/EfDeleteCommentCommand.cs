using Application.UseCases.Commands.Comments;
using DataAccess;
using Domain;
using Implementation.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Comments
{
    public class EfDeleteCommentCommand : EfGenericDeleteCommand<Comment>, IDeleteCommentCommand
    {
        public EfDeleteCommentCommand(HotelHorizonContext context)
            : base(context)
        {
        }

        public override int Id => 29;

        public override string Name => "Delete comment";
    }
}
