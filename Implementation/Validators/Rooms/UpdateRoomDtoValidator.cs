using Application.DTO.Rooms;
using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Rooms
{
    public class UpdateRoomDtoValidator : AbstractValidator<UpdateRoomDTO>
    {
        public UpdateRoomDtoValidator(HotelHorizonContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(4)
                .WithMessage("Room name must have at least 4 characters.")
                .Must((dto, name) => !context.Rooms.Any(room => room.Name == name && room.Id != dto.Id))
                .WithMessage("Room with this name already exists, try something else please");
                //.Must((dto, name) =>
                //{
                //    var existingRoom = context.Rooms.FirstOrDefault(room => room.Id == dto.Id);
                //    return existingRoom != null && existingRoom.Name != name;
                //})
                //.WithMessage("Room name must be different from the existing one.");


            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MinimumLength(10)
                .WithMessage("Description must have at least 10 characters.")
                .MaximumLength(450)
                .WithMessage("Description must have at most 450 characters.");
                //.Must((dto, description) =>
                //{
                //    var existingRoom = context.Rooms.FirstOrDefault(room => room.Id == dto.Id);
                //    return existingRoom != null && existingRoom.Description != description;
                //})
                //.WithMessage("Description must be different from the existing one.");

            RuleFor(x => x.Price).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.")
                .LessThan(100000)
                .WithMessage("Price must be less than 100000 EUR.");
            //.Must((dto, price) =>
            //{
            //    var existingRoom = context.Rooms.Include(room => room.Prices).FirstOrDefault(room => room.Id == dto.Id);
            //    return existingRoom != null && existingRoom.Prices.All(price => price.RoomPrice != dto.Price);
            //})
            //.WithMessage("Price must be different from the existing one.");

            RuleFor(x => x.Size).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Size is required.")
                .GreaterThan(0)
                .WithMessage("Size must be greater than 0.")
                .LessThan(1000)
                .WithMessage("Size must be less than 1000 square meters.");
                //.Must((dto, size) =>
                //{
                //    var existingRoom = context.Rooms.FirstOrDefault(room => room.Id == dto.Id);
                //    return existingRoom != null && existingRoom.Size != size;
                //})
                //.WithMessage("Size must be different from the existing one.");

            RuleFor(x => x.MainImage).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Main image is required.")
                .Must((x, fileName) =>
                {
                    string path = Path.Combine("wwwroot", "temp", fileName);
                    string destination = Path.Combine("wwwroot", "images", "rooms", fileName);
                    if(Path.Exists(path) || Path.Exists(destination))
                        {
                        return true;
                        }
                    return false;
                })
                .WithMessage("File doesn't exist.")
                .Must((dto, fileName) =>
                {
                    var image = context.Images.FirstOrDefault(img => img.Path == $"/images/rooms/{fileName}");
                    return image == null || !context.Rooms.Any(room => room.MainImageId == image.Id && room.Id != dto.Id);
                })
                .WithMessage("Image with this name is already set as main image for another room.");

            RuleFor(x => x.Images)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Images are required.")
                .Must(images => images.Count <= 5)
                .WithMessage("Room can have at most 5 images.")
                .Must(images =>
                {
                    return images.All(image => File.Exists(Path.Combine("wwwroot", "temp", image)) || File.Exists(Path.Combine("wwwroot", "images", "rooms", image)));
                })
                .WithMessage("One or more image files don't exist.")
                .Must(images =>
                {
                    return images.Distinct().Count() == images.Count;
                })
                .WithMessage("Some images are duplicated.");

            RuleFor(x => x.TypeId).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Room type is required.")
                //valid typeId
                .Must((dto, typeId) =>
                {
                    return context.Types.Any(type => type.Id == typeId && type.IsActive);
                })
                .WithMessage("Room type with this id doesn't exist.");

            RuleFor(x => x.ServiceIds).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("At least one service is required.")
                .Must(services =>
                {
                    return services.All(serviceId => context.Services.Any(service => service.Id == serviceId && service.IsActive));
                })
                .WithMessage("One or more services don't exist.");

            RuleFor(x => x.Beds).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("At least one bed is required.")
               .DependentRules(() =>
               {
                   RuleForEach(x => x.Beds).Must((x, bed) =>
                   {
                       return context.Beds.Any(b => b.Id == bed.BedId);
                   }).WithMessage("Bed with some of the ids doesn't exist.");
               });


        }
    }
}
