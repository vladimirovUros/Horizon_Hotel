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
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(60);
            
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Username)
                   .IsRequired()
                   .HasMaxLength(45);
            
            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(35);

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => new { x.FirstName, x.LastName, x.Email, x.Username })
                   .IncludeProperties(x => new { x.DateOfBirth });

            builder.Property(x => x.Password)
                   .IsRequired();

            builder.HasOne(x => x.Image)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
