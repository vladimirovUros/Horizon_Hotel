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
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDTO>
    {
        public UpdateCommentDtoValidator(HotelHorizonContext context)
        {
            RuleFor(x => x.Text)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Text is required")
                .MinimumLength(3)
                .WithMessage("Text must have at least 3 characters")
                .MaximumLength(450)
                .WithMessage("Text can't have more than 450 characters");
        }
    }
}
