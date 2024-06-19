using Application.DTO.Rooms;
using DataAccess;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Rooms
{
    public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDTO>
    {
        public CreateRoomDtoValidator(HotelHorizonContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Room name must have at least 3 characters.")
                .Must((dto, name) => !context.Rooms.Any(room => room.Name == name))
                .WithMessage(dto => $"Room name '{dto.Name}' is already taken. Please choose a different name.");


            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MinimumLength(10)
                .WithMessage("Description must have at least 10 characters.")
                .MaximumLength(450)
                .WithMessage("Description must have at most 450 characters.");

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.")
                .LessThan(100000)
                .WithMessage("Price must be less than 100000 EUR.");

            RuleFor(x => x.Size)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Size is required.")
                .GreaterThan(0)
                .WithMessage("Size must be greater than 0.")
                .LessThan(1000)
                .WithMessage("Size must be less than 1000 square meters.");

            RuleFor(x => x.MainImage)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Main image is required.")
                //.Must((dto, fileName) =>
                //{
                //    string path = Path.Combine("wwwroot", "temp", fileName);
                //    return File.Exists(path);
                //}).WithMessage("File doesn't exist.")
                .Must((dto, fileName) =>
                {
                    Image image = context.Images.FirstOrDefault(img => img.Path == fileName);
                    return image == null || !context.Rooms.Any(room => room.MainImageId == image.Id);
                }).WithMessage("Image with this name is already set as main image for another room.");

            RuleFor(x => x.Images)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Images are required.")
                .Must(images => images.Count <= 5)
                .WithMessage("Room can have at most 5 images.")
                .Must(images =>
                {
                    return images.All(image => File.Exists(Path.Combine("wwwroot", "temp", image)));
                }).WithMessage("Some files don't exist.")
                .Must(images =>
                {
                    return images.Distinct().Count() == images.Count;
                }).WithMessage("Some images are duplicated.")
            .Must((dto, images) =>
                {
                    return images.Contains(dto.MainImage);
                }).WithMessage("Main image must be in images.");

            RuleFor(x => x.TypeId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Room type is required.")
                .Must(roomType => context.Types.Any(type => type.Id == roomType && type.IsActive))
                .WithMessage("Room type with this id doesn't exist.");

            RuleFor(x => x.ServiceIds)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Services are required.")
                .Must(serviceIds => serviceIds.All(serviceId => context.Services.Any(service => service.Id == serviceId && service.IsActive)))
                .WithMessage("Service with some of the ids doesn't exist.");

            RuleFor(x => x.Beds)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Beds are required.")
                .Must(beds => beds.All(bed => bed.Quantity > 0))
                .WithMessage("Quantity of beds must be greater than 0.")
                .Must(beds => beds.Select(bed => bed.BedId).Distinct().Count() == beds.Count)
                .WithMessage("Some beds are duplicated.")
                .Must(beds => beds.All(bed => context.Beds.Any(b => b.Id == bed.BedId)))
                .WithMessage("Bed with some of the ids doesn't exist.");
        }
    }
}
