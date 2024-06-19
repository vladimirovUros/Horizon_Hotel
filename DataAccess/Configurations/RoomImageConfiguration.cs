using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class RoomImageConfiguration : IEntityTypeConfiguration<RoomImage>
    {
        public void Configure(EntityTypeBuilder<RoomImage> builder)
        {
            builder.HasKey(x => new { x.RoomId, x.ImageId });

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.RoomImages)
                   .HasForeignKey(x => x.RoomId);

            builder.HasOne(x => x.Image)
                   .WithMany(x => x.RoomImages)
                   .HasForeignKey(x => x.ImageId);
        }
    }
}
