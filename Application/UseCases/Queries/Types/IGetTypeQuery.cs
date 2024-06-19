using Application.DTO.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Types
{
    public interface IGetTypeQuery : IQuery<TypeDTO, int>
    {
    }
}
