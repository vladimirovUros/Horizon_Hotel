using Application.DTO.Reservations;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles.Reservations
{
    public class GetReservationsProfile : Profile
    {
        public GetReservationsProfile() 
        {
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(
                    dest => dest.RoomName,
                    opt => opt.MapFrom(x => x.Room.Name)
                )
                .ForMember(
                    dest => dest.CheckIn,
                    opt => opt.MapFrom(src => src.CheckIn)
                )
                .ForMember(
                    dest => dest.CheckOut,
                    opt => opt.MapFrom(src => src.CheckOut)
                )
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId)
                );
        }
    }
}
