using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetcareDoctorsAPI.Data.Entities
{
    public partial class NetcareDoctorsContext : DbContext
    {
        public NetcareDoctorsContext()
        {
        }

        public NetcareDoctorsContext(DbContextOptions<NetcareDoctorsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessToken> AccessTokens { get; set; } = null!;
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<Discipline> Disciplines { get; set; } = null!;
        public virtual DbSet<DoctorProfile> DoctorProfiles { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Title> Titles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(StaticClass.DatabaseHelper.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessToken>(entity =>
            {
                entity.ToTable("AccessToken");

                entity.Property(e => e.AccessTokenId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccessTokenValue)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("ApplicationUser");

                entity.Property(e => e.ApplicationUserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApplicationUserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.ToTable("Discipline");

                entity.Property(e => e.DisciplineId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DisciplineName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DoctorProfile>(entity =>
            {
                entity.ToTable("DoctorProfile");

                entity.Property(e => e.DoctorProfileId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DeletionDate).HasColumnType("datetime");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hpcsano)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HPCSANo");

                entity.Property(e => e.Idno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDNo");

                entity.Property(e => e.LastModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.DoctorProfileCreatorNavigations)
                    .HasForeignKey(d => d.Creator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorProfile_ApplicationUser");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DoctorProfileDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorProfile_ApplicationUser2");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.DoctorProfiles)
                    .HasForeignKey(d => d.DisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorProfile_Discipline");

                entity.HasOne(d => d.LastModifierNavigation)
                    .WithMany(p => p.DoctorProfileLastModifierNavigations)
                    .HasForeignKey(d => d.LastModifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorProfile_ApplicationUser1");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.DoctorProfiles)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorProfile_Province");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.DoctorProfiles)
                    .HasForeignKey(d => d.TitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorProfile_Title");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.ProvinceId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.ToTable("Title");

                entity.Property(e => e.TitleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TitleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
