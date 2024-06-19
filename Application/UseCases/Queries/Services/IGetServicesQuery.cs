using Application.DTO;
using Application.DTO.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Services
{
    public interface IGetServicesQuery : IQuery<PagedResponse<ServiceDTO>, SearchService>
    {
    }
}
