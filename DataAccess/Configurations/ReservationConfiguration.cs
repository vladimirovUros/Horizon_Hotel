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
    public class ReservationConfiguration : EntityConfiguration<Reservation>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(x => x.PhoneNumber);

            builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => x.FullName);

            builder.HasIndex(x => x.CheckIn);
            builder.HasIndex(x => x.CheckOut);

            builder.HasIndex(x => new { x.CheckIn, x.CheckOut }).IncludeProperties(x => x.FullName);

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.RoomId);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.UserId);

        }
    }
}
