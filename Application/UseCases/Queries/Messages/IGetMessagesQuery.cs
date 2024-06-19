
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Application.DTO.Messages;

namespace Application.UseCases.Queries.Messages
{
    public interface IGetMessagesQuery : IQuery<PagedResponse<MessageDTO>, SearchMessage>
    {
    }
}
