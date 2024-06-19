using Application.DTO;
using Application.DTO.Users;
using Application.UseCases.Queries.Users;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Users
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        private readonly IMapper _mapper;
        public EfGetUsersQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Search users";

        public PagedResponse<UserDTO> Execute(SearchUser search)
        {
            IQueryable<User> query = Context.Users.Include(x => x.Image).Include(x => x.Reservations).AsQueryable();

            if(!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(search.Keyword.ToLower()) ||
                                                      x.LastName.ToLower().Contains(search.Keyword.ToLower()) || 
                                                      x.Username.ToLower().Contains(search.Keyword.ToLower()));
            }
            if(search.DateOfBirthFrom.HasValue)
            {
                query = query.Where(x => x.DateOfBirth >= search.DateOfBirthFrom);
            }
            if(search.DateOfBirthTo.HasValue)
            {
                query = query.Where(x => x.DateOfBirth <= search.DateOfBirthTo);
            }
            if(search.MinimumNumberOfReservations.HasValue)
            {
                query = query.Where(x => x.Reservations.Count() >= search.MinimumNumberOfReservations);
            }
            if (search.IsActive.HasValue)
            {
                if(search.IsActive.Value)
                {
                    query = query.Where(x => x.IsActive);
                }
                else
                {
                    query = query.Where(x => !x.IsActive);
                }
            }
            return query.Paged<UserDTO, User>(search, _mapper);
        }
    }
}
