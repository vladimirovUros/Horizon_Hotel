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
    public class PriceConfiguration : EntityConfiguration<Price>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Price> builder)
        {
            builder.Property(x => x.RoomPrice).IsRequired();

            builder.HasIndex(x => x.RoomPrice);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.Prices)
                   .HasForeignKey(x => x.RoomId);
        }
    }
}
