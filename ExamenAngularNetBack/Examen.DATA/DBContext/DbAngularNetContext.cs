using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Examen.ENTITY;

public partial class DbAngularNetContext : DbContext
{
    public DbAngularNetContext()
    {
    }

    public DbAngularNetContext(DbContextOptions<DbAngularNetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserProduct> CustomerProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductShop> ProductShops { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83F9F7E7666");

            entity.ToTable("customer_product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.CustomerProducts)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK__customer___idCus__2E1BDC42");

             entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.CustomerProducts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__customer___idPro__2F10007B");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FC19E1938");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Img).HasColumnName("img");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<ProductShop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83FF8AD260B");

            entity.ToTable("product_shop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.IdShop).HasColumnName("idShop");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductShops)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__product_s__idPro__2B3F6F97");

            entity.HasOne(d => d.IdShopNavigation).WithMany(p => p.ProductShops)
                .HasForeignKey(d => d.IdShop)
                .HasConstraintName("FK__product_s__idSho__2A4B4B5E");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__shop__3213E83F3C9AE311");

            entity.ToTable("shop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Branch)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("branch");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FA04205B7");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Passsword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("passsword");
            entity.Property(e => e.Users)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
