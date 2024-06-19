using Application.DTO.Services;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles.Services
{
    public class GetServiceProfile : Profile
    {
        public GetServiceProfile()
        {
            CreateMap<Service, ServiceDTO>()
                .ForMember(dto => dto.IconPath, opt => opt.MapFrom(service => service.Icon.Path));
        }
    }
}
