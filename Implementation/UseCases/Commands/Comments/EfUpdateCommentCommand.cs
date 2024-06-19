using Application.DTO.Comments;
using Application.Exceptions;
using Application.UseCases.Commands.Comments;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Comments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Comments
{
    public class EfUpdateCommentCommand : EfUseCase, IUpdateCommentCommand
    {
        private readonly UpdateCommentDtoValidator _validator;
        private readonly IMapper _mapper;
        public EfUpdateCommentCommand(HotelHorizonContext context, IMapper mapper, UpdateCommentDtoValidator validator) : base(context)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public int Id => 28;

        public string Name => "Update comment";

        public void Execute(UpdateCommentDTO data)
        {
            _validator.ValidateAndThrow(data);

            Comment comment = Context.Comments.Include(x => x.Author)
                                              .FirstOrDefault(x => x.Id == data.Id) ?? throw new EntityNotFoundException(nameof(Comment), data.Id);
            
            _mapper.Map(data, comment);
            
            Context.SaveChanges();
        }
    }
}
