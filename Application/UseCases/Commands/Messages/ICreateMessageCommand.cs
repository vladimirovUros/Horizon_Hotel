using Application.DTO.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Messages
{
    public interface ICreateMessageCommand : ICommand<CreateMessageDTO>
    {
    }
}
