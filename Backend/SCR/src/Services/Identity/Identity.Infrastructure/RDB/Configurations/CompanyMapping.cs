using Identity.Domain.Aggregates.Company;
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
            builder.HasMany<CompanySetting>().WithOne().HasForeignKey(t=>t.CompanyId);
            builder.Property(t => t.Code).HasMaxLength(50);
        }
    }

    public class COmpanySetttingMapping : IEntityTypeConfiguration<CompanySetting>
    {
        public void Configure(EntityTypeBuilder<CompanySetting> builder)
        {
            builder.ToTable("CompanySetting");
            builder.HasKey(t=>new{t.CompanyId,t.Key});
            
        }
    }
}
