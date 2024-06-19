using Application.DTO;
using Application.DTO.Beds;
using Application.DTO.Rooms;
using Application.DTO.Services;
using Application.UseCases.Queries.Rooms;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Rooms
{
    public class EfGetRoomsQuery : EfUseCase, IGetRoomsQuery
    {
        public EfGetRoomsQuery(HotelHorizonContext context) 
            : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Search rooms";

        public PagedResponse<RoomDTO> Execute(SearchRoom search)
        {
            IQueryable<Room> query = Context.Rooms.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }
            if (search.Size.HasValue)
            {
                query = query.Where(x => x.Size <= search.Size);
            }
            if (search.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Prices.Where(x => x.DateTo == null && x.IsActive).Any(p => p.RoomPrice <= search.MaxPrice));
            }
            if (search.MinPrice.HasValue)
            {
                query = query.Where(x => x.Prices.Where(x => x.DateTo == null && x.IsActive).Any(p => p.RoomPrice >= search.MinPrice));
            }
            if (search.TypeId.HasValue)
            {
                query = query.Where(x => x.TypeId == search.TypeId.Value); 
            }
            if (search.BedsId.Count != 0)
            {
                query = query.Where(x => x.RoomBeds.Any(rb => search.BedsId.Contains(rb.BedId)));
            }
            if (search.ServicesId.Count != 0)
            {
                query = query.Where(x => x.RoomServices.Any(rs => search.ServicesId.Contains(rs.ServiceId)));
            }
            if (search.DateFrom.HasValue && search.DateTo.HasValue)
            {
                DateTime startDate = search.DateFrom.Value.Date;
                DateTime endDate = search.DateTo.Value.Date;

                query = query.Where(x => !x.OccupiedRooms.Any(y => y.Date >= startDate && y.Date <= endDate));
            }
            if (search.Guests.HasValue)
            {
                query = query.Where(x => x.Type.Capacity >= search.Guests);
            }
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;

            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            //16 PerPage = 5, Page = 2

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);
            return new PagedResponse<RoomDTO>
            {
                CurrentPage = page,
                PerPage = perPage,
                TotalCount = totalCount,
                Data = query.Select(x => new RoomDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Size = x.Size,
                    RoomType = x.Type.Name,
                    Beds = x.RoomBeds.Select(b => new BedDTO
                    {
                        Id = b.BedId,
                        Name = b.Bed.Name,
                        TotalQuantity = b.Quantity 
                    }).ToList(),
                    Services = x.RoomServices.Select(s => new ServiceDTO
                    {
                        Id = s.ServiceId,
                        Name = s.Service.Name,
                        IconPath = s.Service.Icon.Path
                    }).ToList(),
                    MainImage = x.MainImage != null ? x.MainImage.Path : null,
                    Price = x.Prices.FirstOrDefault(p => p.DateTo == null && p.IsActive).RoomPrice,
                    Description = x.Description,
                    Images = x.RoomImages.Select(i => i.Image.Path).ToList(),
                    Comments = x.Comments.Select(x => new CommentDTO
                    {
                        Id = x.Id,
                        Text = x.Text,
                        Author = x.Author.FirstName + " " + x.Author.LastName,
                        CommentedAt = x.CreatedAt
                    }).ToList(),
                    Guests = x.Type.Capacity
                }).ToList()
            };

        }
    }
}
