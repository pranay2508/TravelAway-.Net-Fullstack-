using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infosys.TravelAway.DAL.Models;

public partial class RentalSystemDbContext : DbContext
{
    public RentalSystemDbContext()
    {
    }

    public RentalSystemDbContext(DbContextOptions<RentalSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AvailabilityType> AvailabilityTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<FurnishingType> FurnishingTypes { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<PropertyInterest> PropertyInterests { get; set; }

    public virtual DbSet<PropertyType> PropertyTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PRANAY-BHATNAGA\\\\SQLEXPRESS,49233;Database=RentalSystemDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailabilityType>(entity =>
        {
            entity.HasKey(e => e.AvailabilityTypeId).HasName("PK__Availabi__7A4AFFCA9CC3D7FF");

            entity.ToTable("AvailabilityType");

            entity.HasIndex(e => e.AvailabilityStatus, "UQ__Availabi__07843E355F67C9DF").IsUnique();

            entity.Property(e => e.AvailabilityStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D823983CC0");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.EmailId, "UQ__Customer__7ED91ACEF33FC57F").IsUnique();

            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FurnishingType>(entity =>
        {
            entity.HasKey(e => e.FurnishingTypeId).HasName("PK__Furnishi__825C02B710C0F785");

            entity.ToTable("FurnishingType");

            entity.HasIndex(e => e.FurnishingStatus, "UQ__Furnishi__89AA08BB635B18FB").IsUnique();

            entity.Property(e => e.FurnishingStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__Owner__819385B855DD21E2");

            entity.ToTable("Owner");

            entity.HasIndex(e => e.EmailId, "UQ__Owner__7ED91ACE5932A15D").IsUnique();

            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PK__Properti__70C9A735153CCC54");

            entity.Property(e => e.AdditionalNotes)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Available");

            entity.HasOne(d => d.AvailabilityType).WithMany(p => p.Properties)
                .HasForeignKey(d => d.AvailabilityTypeId)
                .HasConstraintName("FK__Propertie__Avail__48CFD27E");

            entity.HasOne(d => d.FurnishingType).WithMany(p => p.Properties)
                .HasForeignKey(d => d.FurnishingTypeId)
                .HasConstraintName("FK__Propertie__Furni__47DBAE45");

            entity.HasOne(d => d.Owner).WithMany(p => p.Properties)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Propertie__Owner__45F365D3");

            entity.HasOne(d => d.PropertyType).WithMany(p => p.Properties)
                .HasForeignKey(d => d.PropertyTypeId)
                .HasConstraintName("FK__Propertie__Prope__46E78A0C");
        });

        modelBuilder.Entity<PropertyInterest>(entity =>
        {
            entity.HasKey(e => e.InterestId).HasName("PK__Property__20832C670FED5DC1");

            entity.ToTable("PropertyInterest");

            entity.HasIndex(e => new { e.PropertyId, e.CustomerId }, "UC_PropertyCustomer").IsUnique();

            entity.Property(e => e.LastFollowUpDate).HasColumnType("datetime");
            entity.Property(e => e.SharedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.PropertyInterests)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__PropertyI__Custo__4F7CD00D");

            entity.HasOne(d => d.Property).WithMany(p => p.PropertyInterests)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__PropertyI__Prope__4E88ABD4");
        });

        modelBuilder.Entity<PropertyType>(entity =>
        {
            entity.HasKey(e => e.PropertyTypeId).HasName("PK__Property__BDE14DB498971C04");

            entity.ToTable("PropertyType");

            entity.HasIndex(e => e.TypeName, "UQ__Property__D4E7DFA871C37C20").IsUnique();

            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
