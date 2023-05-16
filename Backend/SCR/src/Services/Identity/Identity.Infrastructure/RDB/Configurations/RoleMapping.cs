using Identity.Domain.Aggregates.Role;
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
            builder.Property(t => t.Code).HasMaxLength(50);
           
        }

    }
    public class RolePermissionRelationMapping : IEntityTypeConfiguration<RolePermissionRelation>
    {
        public void Configure(EntityTypeBuilder<RolePermissionRelation> builder)
        {
            builder.ToTable("RolePermissionRelation");
            builder.HasKey(t => new {
                t.RoleId,
                t.PermissionId
            });
        }
    }


}
