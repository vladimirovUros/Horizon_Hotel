using Application.DTO;
using Application.DTO.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Types
{
    public interface IGetTypesQuery : IQuery<PagedResponse<TypeDTO>, SearchType>
    {
    }
}
