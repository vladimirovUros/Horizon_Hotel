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
    public class CommentConfiguration : EntityConfiguration<Comment>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Text)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.HasOne(x => x.Author)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.RoomId);
        }
    }
}
