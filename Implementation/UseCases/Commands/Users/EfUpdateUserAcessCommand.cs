using Application.DTO.UserUseCases;
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
    public class EfUpdateUserAcessCommand : EfUseCase, IUpdateUseAccessCommand
    {
        private readonly UpdateUserAccessDtoValidator _validator;
        public EfUpdateUserAcessCommand(HotelHorizonContext context, UpdateUserAccessDtoValidator validator) 
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 40;

        public string Name => "Update user access";

        public void Execute(UpdateUserAccessDto data)
        {
            _validator.ValidateAndThrow(data);

            List<UserUseCase> userUseCases = Context.UserUseCases.Where(u => u.UserId == data.UserId).ToList();

            Context.UserUseCases.RemoveRange(userUseCases);

            Context.UserUseCases.AddRange(data.UseCaseIds.Select(id => new UserUseCase
            {
                UserId = data.UserId,
                UseCaseId = id
            }));

            Context.SaveChanges();
        }
    }
}
