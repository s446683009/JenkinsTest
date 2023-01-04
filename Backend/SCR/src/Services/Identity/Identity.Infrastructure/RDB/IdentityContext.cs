﻿using Identity.Domain.Aggregates.Company;
using Identity.Domain.Aggregates.User;
using Identity.Domain.Aggregates.Role;
using Identity.Domain.Aggregates.Permission;
using Identity.Domain.SeedWork;
using Identity.Infrastructure.RDB.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Infrastructure.RDB
{
    public class IdentityContext : DbContext, IUnitOfWork
    {

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Company> Permissons { get; set; }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("UserId")
            .StartsAt(1).HasMin(1).HasMax(int.MaxValue);
            modelBuilder.HasSequence<int>("CompanyId")
            .StartsAt(1).HasMin(1).HasMax(int.MaxValue);
            modelBuilder.HasSequence<int>("RoleId")
            .StartsAt(1).HasMin(1).HasMax(int.MaxValue);
            modelBuilder.HasSequence<int>("PermissionId")
            .StartsAt(1).HasMin(1).HasMax(int.MaxValue);
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new CompanyMapping());
            modelBuilder.ApplyConfiguration(new PermissionMapping());
            modelBuilder.ApplyConfiguration(new UserCompanyRelationMapping());
            modelBuilder.ApplyConfiguration(new UserRoleRelationMapping());
            modelBuilder.ApplyConfiguration(new  RolePermissionRelationMapping());
            modelBuilder.ApplyConfiguration(new COmpanySetttingMapping());
            base.OnModelCreating(modelBuilder);
        }
       
    }
}
