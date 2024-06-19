using Application.DTO.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users
{
    public class UpdateUserImageValidator : AbstractValidator<InsertUserImageDTO>
    {
        public UpdateUserImageValidator()
        {
            RuleFor(x => x.File)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("At least one image is required.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.File).Must((x, fileName) =>
                    {
                        string path = Path.Combine("wwwroot", "temp", fileName);

                        return Path.Exists(path);

                    }).WithMessage("File doesn't exist.");
                });
        }
    }
}
