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
    public class RoomBedConfiguration : IEntityTypeConfiguration<RoomBed>
    {
        public void Configure(EntityTypeBuilder<RoomBed> builder)
        {
            builder.HasKey(x => new { x.RoomId, x.BedId });
            
            builder.Property(x => x.Quantity)
                   .IsRequired();

            builder.HasOne(x => x.Bed)
                   .WithMany(x => x.RoomBeds)
                   .HasForeignKey(x => x.BedId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.RoomBeds)
                   .HasForeignKey(x => x.RoomId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
