using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Commands.Users
{
    public interface IUpdateUserImageCommand : ICommand<InsertUserImageDTO>
    {
    }
}
