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
    public class OccupiedRoomConfiguration : IEntityTypeConfiguration<OccupiedRoom>
    {
        public void Configure(EntityTypeBuilder<OccupiedRoom> builder)
        {
            builder.HasIndex(x => x.Date);

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.OccupiedRooms)
                   .HasForeignKey(x => x.RoomId);
        }
    }
}
