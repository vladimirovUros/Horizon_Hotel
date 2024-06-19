using Application;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfUpdateUserImageCommand : EfUseCase, IUpdateUserImageCommand
    {
        private readonly UpdateUserImageValidator _validator;
        private readonly IApplicationActor _actor;
        public EfUpdateUserImageCommand(HotelHorizonContext context, UpdateUserImageValidator validator, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 5;

        public string Name => "Update user image";

        public void Execute(InsertUserImageDTO data)
        {
            User u = Context.Users.Find(_actor.Id);
            if(u == null)
            {
                throw new EntityNotFoundException(nameof(User), _actor.Id);
            }
            _validator.ValidateAndThrow(data);

            var tempFile = Path.Combine("wwwroot", "temp", data.File);
            var destinactionFile = Path.Combine("wwwroot", "images", "users", data.File);
            File.Move(tempFile, destinactionFile);

            u.Image = new Image
            {
                Path = $"/images/users/{data.File}"
            };

            Context.SaveChanges();
        }
    }
}
