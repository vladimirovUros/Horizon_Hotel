using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class TypeConfiguration : NamedEntityConfiguration<Domain.Type>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Domain.Type> builder)
        {
            builder.Property(x => x.Capacity).IsRequired();
        }
    }
}
