using Application.DTO.Comments;
using Application.Exceptions;
using Application.UseCases.Commands.Comments;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Comments
{
    public class EfCreateCommentCommand : EfUseCase, ICreateCommentCommand
    {
        private CreateCommentDtoValidator _validator;
        private readonly IMapper _mapper;
        public EfCreateCommentCommand(HotelHorizonContext context, CreateCommentDtoValidator validator, IMapper mapper) 
            : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 27;

        public string Name => "Create comment";

        public void Execute(CreateCommentDTO data)
        {
            _validator.ValidateAndThrow(data);

            Room room = Context.Rooms.FirstOrDefault(r => r.Id == data.RoomId && r.IsActive) ?? throw new EntityNotFoundException(nameof(Room), data.RoomId);

            Comment comment = _mapper.Map<Comment>(data);

            Context.Comments.Add(comment);

            Context.SaveChanges();
        }
    }
}
