using Application.DTO.Types;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Domain.Type;

namespace Implementation.Profiles.Types
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<Type, TypeDTO>();
            CreateMap<TypeDTO, Type>();
        }
    }
}
