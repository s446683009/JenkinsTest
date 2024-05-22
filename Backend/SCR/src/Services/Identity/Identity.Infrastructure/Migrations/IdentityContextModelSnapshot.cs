﻿// <auto-generated />
using System;
using Identity.Infrastructure.RDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Identity.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityContext))]
    partial class IdentityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.HasSequence<int>("CompanyId")
                .HasMin(1L)
                .HasMax(2147483647L);

            modelBuilder.HasSequence<int>("PermissionId")
                .HasMin(1L)
                .HasMax(2147483647L);

            modelBuilder.HasSequence<int>("RoleId")
                .HasMin(1L)
                .HasMax(2147483647L);

            modelBuilder.HasSequence<int>("UserId")
                .HasMin(1L)
                .HasMax(2147483647L);

            modelBuilder.Entity("Identity.Domain.Aggregates.Company.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"CompanyId\"')");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Company.CompanySetting", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<int?>("CompanyId1")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Remark")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("CompanyId", "Key");

                    b.HasIndex("CompanyId1");

                    b.ToTable("CompanySetting");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Permission.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"PermissionId\"')");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ParentId")
                        .HasColumnType("integer");

                    b.HasKey("PermissionId");

                    b.HasIndex("ParentId");

                    b.ToTable("Permisson");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Role.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"RoleId\"')");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Role.RolePermissionRelation", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.ToTable("RolePermissionRelation");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"UserId\"')");

                    b.Property<string>("Account")
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DefaultCompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Descrption")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsActived")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Mobile")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PswEncryptCode")
                        .HasColumnType("text");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.User.UserCompanyRelation", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CompanyId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCompanyRelation");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.User.UserRoleRelation", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoleRelation");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Company.CompanySetting", b =>
                {
                    b.HasOne("Identity.Domain.Aggregates.Company.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity.Domain.Aggregates.Company.Company", null)
                        .WithMany("Settings")
                        .HasForeignKey("CompanyId1");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Permission.Permission", b =>
                {
                    b.HasOne("Identity.Domain.Aggregates.Permission.Permission", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Identity.Domain.Aggregates.Permission.PermissionAction", "Actions", b1 =>
                        {
                            b1.Property<int>("PermissionId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Action")
                                .HasColumnType("text");

                            b1.Property<int>("PermissionId1")
                                .HasColumnType("integer");

                            b1.HasKey("PermissionId");

                            b1.HasIndex("PermissionId1");

                            b1.ToTable("Permission_Action");

                            b1.WithOwner()
                                .HasForeignKey("PermissionId1");
                        });

                    b.Navigation("Actions");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Role.Role", b =>
                {
                    b.HasOne("Identity.Domain.Aggregates.Company.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Role.RolePermissionRelation", b =>
                {
                    b.HasOne("Identity.Domain.Aggregates.Role.Role", null)
                        .WithMany("PermissionRelations")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.User.User", b =>
                {
                    b.HasOne("Identity.Domain.Aggregates.Role.Role", null)
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.User.UserCompanyRelation", b =>
                {
                    b.HasOne("Identity.Domain.Aggregates.User.User", null)
                        .WithMany("Companies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.User.UserRoleRelation", b =>
                {
                    b.HasOne("Identity.Domain.Aggregates.User.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Company.Company", b =>
                {
                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.Role.Role", b =>
                {
                    b.Navigation("PermissionRelations");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Identity.Domain.Aggregates.User.User", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
