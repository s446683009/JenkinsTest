using Identity.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.RDB.Configurations
{
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");
            builder.HasKey(t=>t.CompanyId);

            builder.Property(t=>t.CompanyId).HasDefaultValueSql("nextval('\"CompanyId\"')"); 
            builder.Property(t => t.Name).IsRequired().HasMaxLength(50);

        }
    }
}
