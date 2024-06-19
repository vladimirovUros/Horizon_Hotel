using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Users
{
    public interface IGetUserQuery : IQuery<UserDTO, int>
    {
    }
}
