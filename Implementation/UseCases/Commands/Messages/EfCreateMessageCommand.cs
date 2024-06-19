using Application.DTO.Messages;
using Application.UseCases.Commands.Messages;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Comments;
using Implementation.Validators.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Messages
{
    public class EfCreateMessageCommand : EfUseCase, ICreateMessageCommand
    {
        private CreateMessageDtoValidator _validator;
        private readonly IMapper _mapper;

        public EfCreateMessageCommand(HotelHorizonContext context, CreateMessageDtoValidator validator, IMapper mapper) 
            : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 32;

        public string Name => "Create message";

        public void Execute(CreateMessageDTO data)
        {
            _validator.ValidateAndThrow(data);

            Message message = _mapper.Map<Message>(data);

            Context.Messages.Add(message);

            Context.SaveChanges();
        }
    }
}
