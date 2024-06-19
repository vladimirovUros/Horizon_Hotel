using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class ServiceConfiguration : NamedEntityConfiguration<Service>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.IconId).IsRequired();
            builder.HasOne(x => x.Icon).WithMany(x => x.Services).HasForeignKey(x => x.IconId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
