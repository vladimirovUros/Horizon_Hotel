using Application.DTO.Rooms;
using Application.Exceptions;
using Application.UseCases.Commands.Rooms;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Domain.Type;

namespace Implementation.UseCases.Commands.Rooms
{
    public class EfUpdateRoomCommand : EfUseCase, IUpdateRoomCommand
    {
        private readonly UpdateRoomDtoValidator _validator;
        public EfUpdateRoomCommand(HotelHorizonContext context, UpdateRoomDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "Update room";

        public void Execute(UpdateRoomDTO data)
        {
            _validator.ValidateAndThrow(data);

            Room room = Context.Rooms.Include(x => x.Prices)
                .Include(x => x.MainImage)
                .Include(x => x.RoomImages)
                .ThenInclude(ri => ri.Image)
                .Include(x => x.RoomBeds)
                .Include(x => x.RoomServices)
                .FirstOrDefault(x => x.Id == data.Id);

            if (room == null)
                throw new EntityNotFoundException(nameof(Room), data.Id);

            room.Name = data.Name;
            room.Size = data.Size;
            room.Description = data.Description;

            Price activePirce = room.Prices.FirstOrDefault(price => price.DateTo == null && price.IsActive && price.RoomId == room.Id);
            if(activePirce.RoomPrice != data.Price)
            {
                activePirce.IsActive = false;
                activePirce.DateTo = DateTime.UtcNow;
            }
            room.Prices.Add(new Price
            {
                RoomPrice = data.Price,
                DateFrom = DateTime.UtcNow,
                DateTo = null,
            });
            Context.SaveChanges();

            room.Type = Context.Types.Find(data.TypeId) ?? throw new EntityNotFoundException(nameof(Type), data.TypeId);

            Image oldImage = room.MainImage;

            if (oldImage.Path != $"/images/rooms/{data.MainImage}")
            {
                var tempFile = Path.Combine("wwwroot", "temp", data.MainImage);
                var destinactionFile = Path.Combine("wwwroot", "images", "rooms", data.MainImage);
                File.Move(tempFile, destinactionFile);

                Context.Images.Remove(oldImage);

                room.MainImage = new Image
                {
                    Path = $"/images/rooms/{data.MainImage}"
                };
            }

            string mainImagePath = room.MainImage.Path;

            var oldImages  = room.RoomImages.Where(r => r.RoomId == room.Id).Select(x => x.Image.Path).ToList();
            var imagesWithouMainImage = data.Images.Where(image => $"/images/rooms/{image}" != mainImagePath).ToList();
            if(oldImages.Count > 0)
            {
                foreach (string img in oldImages)
                {
                    if (!imagesWithouMainImage.Contains(img))
                    {
                        var image = Context.Images.FirstOrDefault(x => x.Path == img);
                        var roomImage = Context.RoomImages.FirstOrDefault(x => x.ImageId == image.Id);
                        Context.Images.Remove(image);
                        Context.RoomImages.Remove(roomImage);
                        if(File.Exists(Path.Combine("wwwroot", "images", "rooms", img)))
                        {
                            File.Delete(Path.Combine("wwwroot", "images", "rooms", img));
                        }
                    }
                }
                foreach (string img in imagesWithouMainImage)
                {
                    if (!oldImages.Contains(img))
                    {
                        var image = new Image
                        {
                            Path = $"/images/rooms/{img}"
                        };
                        Context.Images.Add(image);

                        var roomImage = new RoomImage
                        {
                            Image = image
                        };
                        room.RoomImages.Add(roomImage);

                        Context.SaveChanges();
                    }
                }
                Context.SaveChanges();
            }
            

            //var oldBeds = room.RoomBeds.Where(rb => rb.RoomId == room.Id).ToList();

            Context.RoomBeds.RemoveRange(room.RoomBeds);


            //var oldServices = room.RoomServices.Where(rs => rs.RoomId == room.Id).ToList();

            Context.RoomServices.RemoveRange(room.RoomServices);

            Context.SaveChanges();

            List<RoomBed> newBeds = data.Beds.Select(bed => new RoomBed
            {
                BedId = bed.BedId,
                Quantity = bed.Quantity
            }).ToList();

            room.RoomBeds = newBeds;

            List<RoomService> newServices = data.ServiceIds.Select(serviceId => new RoomService
            {
                ServiceId = serviceId
            }).ToList();

            room.RoomServices = newServices;

            Context.SaveChanges();
        }
    }
}
