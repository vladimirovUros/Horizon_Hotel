using Application.Exceptions;
using Application.UseCases.Commands.Messages;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Messages
{
    public class EfDeleteMessageCommand : EfUseCase, IDeleteMessageCommand
    {
        public EfDeleteMessageCommand(HotelHorizonContext context) 
            : base(context)
        {
        }

        public int Id => 33;

        public string Name => "Delete message";

        public void Execute(int data)
        {
            Message message = Context.Messages.Find(data) ?? throw new EntityNotFoundException(nameof(Message), data);

            Context.Messages.Remove(message);

            Context.SaveChanges();
        }
    }
}
