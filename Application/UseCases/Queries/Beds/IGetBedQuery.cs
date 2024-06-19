using Application.DTO.Beds;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Beds
{
    public interface IGetBedQuery : IQuery<BedDTO, int>
    {
    }
}
