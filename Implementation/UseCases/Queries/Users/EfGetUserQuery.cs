using Application.DTO;
using Application.DTO.Users;
using Application.UseCases.Queries.Users;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Users
{
    public class EfGetUserQuery : EfGenericFindUseCase<UserDTO, User>, IGetUserQuery
    {
        public EfGetUserQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public override int Id => 23;

        public override string Name => "Get user";
        protected override IQueryable<User> IncludeRelatedEntities(IQueryable<User> query)
        {
            return query
             .Include(u => u.Image)
             .Include(u => u.Reservations)
             .Include(u => u.Comments)
             .Include(u => u.UserUseCases);
        }
    }
}
