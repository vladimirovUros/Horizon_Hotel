﻿using Application;
using Application.DTO.Email;
using Application.DTO.Users;
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
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserDtoValidator _validator;
        private readonly IEmailSender _sender;
        private IEnumerable<int> _useCasesForUser = new List<int> { 3, 5, 21, 22, 23, 24, 25, 27, 28, 29, 32, 35, 36, 41 };

        public EfRegisterUserCommand(RegisterUserDtoValidator validator, HotelHorizonContext context, IEmailSender sender)
            : base(context)
        {
            _validator = validator;
            _sender = sender;
        }

        public int Id => 1;

        public string Name => "Register user";

        public void Execute(RegisterUserDTO data)
        {
            _validator.ValidateAndThrow(data);

            User user = new()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Username = data.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                Image = Context.Images.FirstOrDefault(x => x.Path.Contains("defaultuser")),
                DateOfBirth = data.DateOfBirth.Value,
                UserUseCases = _useCasesForUser.Select(x => new UserUseCase { UseCaseId = x }).ToList()
            };

            Context.Users.Add(user);

            Context.SaveChanges();

            _sender.SendEmail(new SendEmailDto
            {
                Subject = "Registration",
                Content = "<h1> Successfull Registration! </h1>",
                SendTo = data.Email
            });
        }
    }
}
