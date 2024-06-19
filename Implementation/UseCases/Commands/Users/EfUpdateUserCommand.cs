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
    public class EfUpdateUserCommand : EfUseCase, IUpdateUserCommand
    {
        private readonly UpdateUserDtoValidator _validator;
        private readonly IApplicationActor _actor;

        public EfUpdateUserCommand(UpdateUserDtoValidator validator, HotelHorizonContext context, IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 3;

        public string Name => "Update user";

        public void Execute(UpdateUserDTO data)
        {
            if(data.Id != _actor.Id)
            {
                throw new ConflictException("You are not allowed to update other users data.");
            }

            _validator.ValidateAndThrow(data);

            User user = Context.Users.Find(data.Id) ?? throw new EntityNotFoundException(nameof(User), data.Id);
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.Email = data.Email;
            user.Username = data.Username;

            Context.SaveChanges();
        }
    }
}
