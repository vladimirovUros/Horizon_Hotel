using Application.DTO.Users;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles.Users
{
    public class GetUserProfile : Profile
    {
        public GetUserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(
                    dest => dest.NumberOfReservations,
                    opt => opt.MapFrom(src => src.Reservations.Count)
                )
                .ForMember(
                    dest => dest.ImagePath,
                    opt => opt.MapFrom(src => src.Image.Path)
                );
            CreateMap<UserDTO, User>()
                .ForMember(
                    dest => dest.Reservations,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Image,
                    opt => opt.Ignore()
                );
        }
    }
}
