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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(x => x.TextMessage)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.DateOfSend)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
