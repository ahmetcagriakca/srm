using Fix.Security;
using IdentityServer.Models;
using IdentityServer.Models.PageManagement;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentityServer.Infrastructor
{
    public class IdentityServerDbContext : DbContext
    {
        public Guid ID
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.TraceableEntity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.UserName).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Surname).HasMaxLength(50).IsRequired();
                entity.Property(x => x.IsActive);
                entity.HasOne(x => x.Company);
                entity.HasIndex(x => x.UserName).IsUnique();
            });

            modelBuilder.TraceableEntity<Company>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.ApiUrl);
                entity.Property(x => x.Name);
                entity.Property(x => x.SystemName);
                entity.HasMany(x => x.Users).WithOne(u => u.Company).IsRequired().OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.TraceableEntity<Role>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("PK_RoleId");
            });

            modelBuilder.TraceableEntity<UserInRole>(entity =>
            {
                entity.HasKey("RoleId", "UserId").HasName("PK_UserInRole");
                entity.HasOne(x => x.Role).WithMany(x => x.UserInRoles).HasForeignKey("RoleId").HasConstraintName("FK_UserInRole_Role");
                entity.HasOne(x => x.User).WithMany(x => x.UserInRoles).HasForeignKey("UserId").HasConstraintName("FK_UserInRole_User");
            });

            modelBuilder.TraceableEntity<RolePermission>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Role).WithMany(u => u.Permissions).IsRequired().OnDelete(DeleteBehavior.Restrict);
                entity.Property(x => x.Permission).IsRequired();
            });

            modelBuilder.TraceableEntity<UserSession>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.User);
                entity.HasOne(x => x.Company);
                entity.Property(x => x.Token).IsRequired();
                entity.Property(x => x.LastLoginOn).IsRequired();
                entity.Property(x => x.Channel);
                entity.Property(x => x.DeviceId);
                entity.Property(x => x.TimeOut);
                entity.Property(x => x.IsActive);
            });

            modelBuilder.TraceableEntity<AuthenticationEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.UserId).IsRequired();
                entity.Property(x => x.Token).HasMaxLength(512).IsRequired();
                entity.Property(x => x.RefreshToken).HasMaxLength(512).IsRequired();
                entity.Property(x => x.ExpiredOn).IsRequired();
                entity.Property(x => x.IsActive).IsRequired();
            });



            #region Application Management

            modelBuilder.TraceableEntity<Page>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Url).HasMaxLength(250).IsRequired();
                entity.Property(x => x.Name).HasMaxLength(500).IsRequired();
                entity.Property(x => x.ComponentName).HasMaxLength(100);
                entity.Property(x => x.Order);
                entity.Property(x => x.Icon).HasMaxLength(50);
                entity.Property(x => x.ShowOnMenu).IsRequired();
                entity.HasMany(j => j.Children)
            .WithOne(j => j.Parent)
            .HasForeignKey(j => j.ParentId);

            });

            modelBuilder.TraceableEntity<PageRole>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Page).WithMany(x => x.PageRoles)
                    .HasForeignKey(d => d.PageId);
                entity.HasOne(x => x.Role).WithMany(x => x.PageRoles)
                    .HasForeignKey(d => d.RoleId);
            });

            #endregion
        }
    }
}