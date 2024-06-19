using Application.DTO;
using Application.DTO.Beds;
using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Beds
{
    public interface IGetBedsQuery : IQuery<PagedResponse<BedDTO>, SearchBed>
    {
    }
}
