using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CMS_Prototype.Models
{
    public partial class cmsmainContext : DbContext
    {
        public cmsmainContext()
        {
        }

        public cmsmainContext(DbContextOptions<cmsmainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Video> Video { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:cms-grupa-3.database.windows.net,1433;Initial Catalog=cms-main;User ID=cms_admin;Password=useruser0!;MultipleActiveResultSets =False;Encrypt=True;TrustServerCertificate=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Format)
                    .HasColumnName("format")
                    .HasMaxLength(10);

                entity.Property(e => e.VideoLink)
                    .HasColumnName("video_link")
                    .HasMaxLength(255);

                entity.Property(e => e.VideoName)
                    .HasColumnName("video_name")
                    .HasMaxLength(100);
            });
        }
    }
}
