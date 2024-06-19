using Application.DTO.Beds;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles.Beds
{
    public class BedProfile : Profile
    {
        public BedProfile()
        {
            CreateMap<Bed, BedDTO>()
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.RoomBeds.Sum(rb => rb.Quantity)));

            CreateMap<BedDTO, Bed>();
        }
    }
}
