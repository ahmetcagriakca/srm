// <auto-generated />
using System;
using IdentityServer.Infrastructor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IdentityServer.Migrations
{
    [DbContext(typeof(IdentityServerDbContext))]
    [Migration("20191020004642_identity_last_changes")]
    partial class identity_last_changes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Fix.Security.AuthenticationEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpiredOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("character varying(512)")
                        .HasMaxLength(512);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("character varying(512)")
                        .HasMaxLength(512);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("AuthenticationEntity");
                });

            modelBuilder.Entity("IdentityServer.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ApiUrl")
                        .HasColumnType("text");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<string>("SystemName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("IdentityServer.Models.PageManagement.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ComponentName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Icon")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<bool>("ShowOnMenu")
                        .HasColumnType("boolean");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Page");
                });

            modelBuilder.Entity("IdentityServer.Models.PageManagement.PageRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("PageId")
                        .HasColumnType("integer");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.HasIndex("RoleId");

                    b.ToTable("PageRole");
                });

            modelBuilder.Entity("IdentityServer.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id")
                        .HasName("PK_RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("IdentityServer.Models.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("IdentityServer.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("MobilePhone")
                        .HasColumnType("text");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("IdentityServer.Models.UserInRole", b =>
                {
                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("RoleId", "UserId")
                        .HasName("PK_UserInRole");

                    b.HasAlternateKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserInRole");
                });

            modelBuilder.Entity("IdentityServer.Models.UserSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Channel")
                        .HasColumnType("text");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeviceId")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastLoginOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("TimeOut")
                        .HasColumnType("integer");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSession");
                });

            modelBuilder.Entity("IdentityServer.Models.PageManagement.Page", b =>
                {
                    b.HasOne("IdentityServer.Models.PageManagement.Page", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("IdentityServer.Models.PageManagement.PageRole", b =>
                {
                    b.HasOne("IdentityServer.Models.PageManagement.Page", "Page")
                        .WithMany("PageRoles")
                        .HasForeignKey("PageId");

                    b.HasOne("IdentityServer.Models.Role", "Role")
                        .WithMany("PageRoles")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("IdentityServer.Models.RolePermission", b =>
                {
                    b.HasOne("IdentityServer.Models.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityServer.Models.User", b =>
                {
                    b.HasOne("IdentityServer.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityServer.Models.UserInRole", b =>
                {
                    b.HasOne("IdentityServer.Models.Role", "Role")
                        .WithMany("UserInRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserInRole_Role")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityServer.Models.User", "User")
                        .WithMany("UserInRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserInRole_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityServer.Models.UserSession", b =>
                {
                    b.HasOne("IdentityServer.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("IdentityServer.Models.User", "User")
                        .WithMany("UserSessions")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
