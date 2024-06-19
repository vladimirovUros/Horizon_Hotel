using Application.DTO.Beds;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Beds
{
    public interface ICreateBedCommand : ICommand<CreateBedDTO>
    {
    }
}
