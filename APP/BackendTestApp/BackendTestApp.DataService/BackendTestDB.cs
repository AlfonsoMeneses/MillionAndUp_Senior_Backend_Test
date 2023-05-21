using System;
using System.Collections.Generic;
using BackendTestApp.DataService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTestApp.DataService;

public partial class BackendTestDB : DbContext
{
    public BackendTestDB()
    {
    }

    public BackendTestDB(DbContextOptions<BackendTestDB> options)
        : base(options)
    {
    }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<PropertyImage> PropertyImages { get; set; }

    public virtual DbSet<PropertyTrace> PropertyTraces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=MS-AKATSUKI;Database=BackendTest;Uid=MS-AKATSUKI\\alfon;TrustServerCertificate=True;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.IdOwner);

            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.IdProperty);

            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.CodeInternal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.PropertyOwner).WithMany(p => p.Properties)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK_Properties_Owner");
        });

        modelBuilder.Entity<PropertyImage>(entity =>
        {
            entity.HasKey(e => e.IdPropertyImage);

            entity.Property(e => e.Enabled).HasDefaultValueSql("((1))");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPropertyNavigation).WithMany(p => p.PropertyImages)
                .HasForeignKey(d => d.IdProperty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyImages_Property");
        });

        modelBuilder.Entity<PropertyTrace>(entity =>
        {
            entity.HasKey(e => e.IdPropertyTrace);

            entity.Property(e => e.DateSale).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tax).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdPropertyNavigation).WithMany(p => p.PropertyTraces)
                .HasForeignKey(d => d.IdProperty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyTraces_Property");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
