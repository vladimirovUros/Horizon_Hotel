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
    public class RoomConfiguration : NamedEntityConfiguration<Room>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Room> builder)
        {
            builder.Property(x => x.Size)
                   .IsRequired();
            
            builder.Property(x => x.Description)
                   .IsRequired();

            builder.HasIndex(x => x.Description);

            builder.Property(x => x.MainImageId)
                   .IsRequired();

            builder.HasOne(x => x.Type)
                   .WithMany(x => x.Rooms)
                   .HasForeignKey(x => x.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.MainImage)
                   .WithMany(x => x.Rooms)
                   .HasForeignKey(x => x.MainImageId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
