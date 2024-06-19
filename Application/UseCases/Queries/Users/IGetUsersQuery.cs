using Application.DTO;
using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Queries.Users
{
    public interface IGetUsersQuery : IQuery<PagedResponse<UserDTO>, SearchUser>
    {
    }
}
