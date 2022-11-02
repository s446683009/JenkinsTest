using Identity.Domain.Aggregates.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.RDB.Configurations
{
    public class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permisson");
            builder.HasKey(t=>t.PermissionId);
            builder.Property(t => t.PermissionId).HasDefaultValueSql("nextval('\"PermissionId\"')");
            builder.Property(t => t.Name).IsRequired();
            builder.HasOne<Permission>(t => t.Parent).WithMany().HasForeignKey(t => t.ParentId);
            builder.OwnsMany(t => t.Actions, c =>
            {
                c.ToTable("Permission_Action");
                c.HasKey("PermissionId");
                c.Property<string>("Action");
               
            });
        }
    }
}
