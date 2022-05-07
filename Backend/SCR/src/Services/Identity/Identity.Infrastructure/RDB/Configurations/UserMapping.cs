using Identity.Domain.Aggregates.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Identity.Infrastructure.RDB.Configurations
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.UserId);
            builder.Property(t=>t.UserId)
                .HasDefaultValueSql("nextval('\"UserId\"')");
            builder.Property(t => t.Mobile).HasMaxLength(50);
            builder.Property(t => t.Email).HasMaxLength(50);


            builder.HasMany(t=>t.Companies).WithMany(t=>t.Users).UsingEntity(t=> {
                t.ToTable("User_Company");
            });
            builder.HasMany(t => t.Roles).WithMany(t=>t.Users).UsingEntity(t => {
                t.ToTable("User_Role");
            });
        }
    }
}
