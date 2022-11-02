using Identity.Domain.Aggregates;
using Identity.Domain.Aggregates.User;
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
        }
    }
    public class UserCompanyRelationMapping : IEntityTypeConfiguration<UserCompanyRelation>
    {
        public void Configure(EntityTypeBuilder<UserCompanyRelation> builder)
        {
            builder.ToTable("UserCompanyRelation");
            builder.HasKey(t=>new { 
                t.CompanyId,
                t.UserId
            });
        }
    }
    public class UserRoleRelationMapping : IEntityTypeConfiguration<UserRoleRelation>
    {
        public void Configure(EntityTypeBuilder<UserRoleRelation> builder)
        {
            builder.ToTable("UserRoleRelation");
            builder.HasKey(t => new {
                t.RoleId,
                t.UserId
            });
        }
    }
}
