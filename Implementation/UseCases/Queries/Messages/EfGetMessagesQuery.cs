using Application.DTO;
using Application.DTO.Messages;
using Application.UseCases.Queries.Messages;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Messages
{
    public class EfGetMessagesQuery : EfUseCase, IGetMessagesQuery
    {
        private readonly IMapper _mapper;
        public EfGetMessagesQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 34;

        public string Name => "Search messages";

        public PagedResponse<MessageDTO> Execute(SearchMessage search)
        {
            IQueryable<Domain.Message> query = Context.Messages.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.TextMessage.ToLower().Contains(search.Keyword.ToLower()));
            }
            if (search.DateOfSend.HasValue)
            {
                query = query.Where(x => x.DateOfSend >= search.DateOfSend);
            }
            return query.Paged<MessageDTO, Message>(search, _mapper);
        }
    }
}
