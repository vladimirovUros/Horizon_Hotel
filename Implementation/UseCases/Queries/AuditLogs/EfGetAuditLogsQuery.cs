using Application.DTO;
using Application.DTO.AuditLogs;
using Application.UseCases.Queries.AuditLogs;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.AuditLogs
{
    public class EfGetAuditLogsQuery : EfUseCase, IGetAuditLogsQuery
    {
        private readonly IMapper _mapper;
        public EfGetAuditLogsQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 39;

        public string Name => "Search audit logs";

        public PagedResponse<AuditLogDTO> Execute(SearchAuditLog search)
        {
            IQueryable<AuditLog> query = Context.AuditLogs.OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(search.Actor) || !string.IsNullOrWhiteSpace(search.Actor))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Actor.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.UseCaseName) || !string.IsNullOrWhiteSpace(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.FromDate) || !string.IsNullOrWhiteSpace(search.FromDate))
            {
                DateTime startDate = Convert.ToDateTime(search.FromDate);
                query = query.Where(x => x.ExecutedAt >= startDate);
            }
            if (!string.IsNullOrEmpty(search.ToDate) || !string.IsNullOrWhiteSpace(search.ToDate))
            {
                DateTime endDate = Convert.ToDateTime(search.ToDate);
                query = query.Where(x => x.ExecutedAt <= endDate);
            }
            return query.Paged<AuditLogDTO, AuditLog>(search, _mapper);
        }
    }
}
