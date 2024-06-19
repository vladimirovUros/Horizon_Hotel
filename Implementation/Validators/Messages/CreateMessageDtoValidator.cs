using Application.DTO.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Messages
{
    public class CreateMessageDtoValidator : AbstractValidator<CreateMessageDTO>
    {
        public CreateMessageDtoValidator()
        {
            RuleFor(x => x.FullName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Full name is required. Example: Novak Djokovic");
            
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not in the right format");

            RuleFor(x => x.TextMessage).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Text message is required")
                .MinimumLength(3)
                .WithMessage("Text message must have at least 3 characters")
                .MaximumLength(100)
                .WithMessage("Text message must have at most 100 characters");
        }
    }
}
