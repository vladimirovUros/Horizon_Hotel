using Application.DTO.AuditLogs;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles.AuditLogs
{
    public class AuditLogProfile : Profile
    {
        public AuditLogProfile()
        {
            CreateMap<AuditLog, AuditLogDTO>()
                .ForMember(dest => dest.Actor, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.UseCaseName, opt => opt.MapFrom(src => src.UseCaseName))
                .ForMember(dest => dest.ExecutedAt, opt => opt.MapFrom(src => src.ExecutedAt))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.UseCaseData));
        }
    }
}
