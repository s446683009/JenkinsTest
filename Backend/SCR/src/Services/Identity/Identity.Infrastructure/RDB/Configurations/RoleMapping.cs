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
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(t => t.RoleId);
            builder.Property(t => t.RoleId).HasDefaultValueSql("nextval('\"RoleId\"')");
            builder.Property(t => t.Name).IsRequired();
            builder.HasOne(t => t.Company).WithMany().HasForeignKey(t => t.CompanyId);
            builder.HasMany(t => t.Permissions).WithMany(t => t.Roles).UsingEntity(t =>t.ToTable("Role_Permission"));
        }
    }
}
