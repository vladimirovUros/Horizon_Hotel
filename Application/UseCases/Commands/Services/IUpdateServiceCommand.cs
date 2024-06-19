using Application.DTO.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Services
{
    public interface IUpdateServiceCommand : ICommand<UpdateServiceDTO>
    {
    }
}
