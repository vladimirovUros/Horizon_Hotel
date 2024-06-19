using Application.DTO.Comments;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Comments
{
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentDtoValidator(HotelHorizonContext context)
        {
            RuleFor(x => x.Text)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Text is required")
                .MinimumLength(3)
                .WithMessage("Text must have at least 3 characters")
                .MaximumLength(450)
                .WithMessage("Text can't have more than 450 characters");

            RuleFor(x => x.RoomId)
                .Cascade(CascadeMode.Stop)
                .Must(id => context.Rooms.Any(u => u.Id == id))
                .WithMessage("Room with the specified ID does not exist.");

            RuleFor(x => x.AuthorId).Cascade(CascadeMode.Stop)
                .Must(id => context.Users.Any(u => u.Id == id))
                .WithMessage("Author with the specified ID does not exist.");
        }
    }
}
