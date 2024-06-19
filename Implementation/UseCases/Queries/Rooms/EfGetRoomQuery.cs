using Application.DTO.Rooms;
using Application.Exceptions;
using Application.UseCases.Queries.Rooms;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.UseCases.Queries.Rooms
{
    public class EfGetRoomQuery : EfGenericFindUseCase<RoomDTO, Room>, IGetRoomQuery
    {
        public EfGetRoomQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public override int Id => 22;

        public override string Name => "Get room";

        protected override IQueryable<Room> IncludeRelatedEntities(IQueryable<Room> query)
        {
            return query
            .Include(r => r.RoomBeds).ThenInclude(rb => rb.Bed)
            .Include(r => r.RoomServices).ThenInclude(rs => rs.Service).ThenInclude(s => s.Icon)
            .Include(r => r.Type)
            .Include(r => r.Prices)
            .Include(r => r.MainImage)
            .Include(r => r.RoomImages).ThenInclude(ri => ri.Image)
            .Include(r => r.Comments).ThenInclude(c => c.Author);
        }
    }
}
