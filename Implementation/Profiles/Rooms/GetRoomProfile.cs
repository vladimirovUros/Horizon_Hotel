using Application.DTO.Beds;
using Application.DTO.Rooms;
using Application.DTO.Services;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles.Posts
{
    public class GetRoomProfile : Profile
    {
        public GetRoomProfile()
        {
            CreateMap<Room, RoomDTO>()
                .ForMember(dto => dto.Beds, opt => opt.MapFrom(room => room.RoomBeds.Select(rb => new BedDTO
                {
                    Id = rb.BedId,
                    Name = rb.Bed.Name,
                    TotalQuantity = rb.Quantity
                })))
                .ForMember(dto => dto.Services, opt => opt.MapFrom(room => room.RoomServices.Select(rs => new ServiceDTO
                {
                    Id = rs.ServiceId,
                    Name = rs.Service.Name,
                    IconPath = rs.Service.Icon.Path
                })))
                .ForMember(dto => dto.RoomType, opt => opt.MapFrom(room => room.Type.Name))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(room => room.Prices.FirstOrDefault(x => x.DateTo == null && x.IsActive).RoomPrice))
                .ForMember(dto => dto.MainImage, opt => opt.MapFrom(room => room.MainImage.Path))
                .ForMember(dto => dto.Images, opt => opt.MapFrom(room => room.RoomImages.Select(ri => ri.Image.Path).ToList()))
                .ForMember(dto => dto.Comments, opt => opt.MapFrom(room => room.Comments.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Text = c.Text,
                    Author = c.Author.FirstName + " " + c.Author.LastName,
                    CommentedAt = c.CreatedAt,
                })))
                .ForMember(dto => dto.Guests, opt => opt.MapFrom(room => room.Type.Capacity));
        }
    }
}
