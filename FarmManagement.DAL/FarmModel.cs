using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FarmManagement.DAL
{
    public partial class FarmModel : DbContext
    {
        public FarmModel()
            : base("name=FarmModel")
        {
            // THÊM DÒNG NÀY ĐỂ TRÁNH LỖI KHI LOAD DỮ LIỆU
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Crop> Crops { get; set; }
        public virtual DbSet<Expens> Expenses { get; set; }
        public virtual DbSet<Farm> Farms { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Revenue> Revenues { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expens>()
                .Property(e => e.Amount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Farm>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.Farm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Farm>()
                .HasMany(e => e.Animals)
                .WithRequired(e => e.Farm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Farm>()
                .HasMany(e => e.Crops)
                .WithRequired(e => e.Farm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Farm>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.Farm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Farm>()
                .HasMany(e => e.Inventories)
                .WithRequired(e => e.Farm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Farm>()
                .HasMany(e => e.Revenues)
                .WithRequired(e => e.Farm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Revenue>()
                .Property(e => e.Amount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.AuditLogs)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
