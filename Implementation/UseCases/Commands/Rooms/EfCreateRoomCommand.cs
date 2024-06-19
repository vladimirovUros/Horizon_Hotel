using Application.DTO.Rooms;
using Application.UseCases.Commands.Rooms;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Rooms
{
    public class EfCreateRoomCommand : EfUseCase, ICreateRoomCommand
    {
        private CreateRoomDtoValidator _validator;
        public EfCreateRoomCommand(HotelHorizonContext context, CreateRoomDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 18;

        public string Name => "Create room";

        public void Execute(CreateRoomDTO data)
        {
            _validator.ValidateAndThrow(data);

            var tempFile = Path.Combine("wwwroot", "temp", data.MainImage);
            var destinactionFile = Path.Combine("wwwroot", "images", "rooms", data.MainImage);
            File.Move(tempFile, destinactionFile);

            string mainImagePath = $"/images/rooms/{data.MainImage}";

            Room room = new()
            {
                Name = data.Name,
                Description = data.Description,
                Size = data.Size,
                Prices = new List<Price>
                {
                    new()
                    {
                        RoomPrice = data.Price,
                        DateFrom = DateTime.UtcNow,
                        DateTo = null,
                    }
                },
                MainImage = new Image
                {
                    Path = mainImagePath
                },

                RoomImages = data.Images.Where(image => $"/images/rooms/{image}" != mainImagePath)
                                        .Select(image => new RoomImage
                                        {
                                            Image = new Image
                                            {
                                                Path = $"/images/rooms/{image}"
                                            }
                                        }).ToList(),
                RoomBeds = data.Beds.Select(bed => new RoomBed
                {
                    BedId = bed.BedId,
                    Quantity = bed.Quantity
                }).ToList(),

                RoomServices = data.ServiceIds.Select(serviceId => new RoomService
                {
                    ServiceId = serviceId
                }).ToList(),
                TypeId = data.TypeId
            };

            Context.Rooms.Add(room);

            Context.SaveChanges();
        }
    }
}
