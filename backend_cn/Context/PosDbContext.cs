using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using backend_cn.Models;

namespace backend_cn.Context
{
    public partial class PosDbContext : DbContext
    {
        public PosDbContext()
        {
        }

        public PosDbContext(DbContextOptions<PosDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Receipt> Receipts { get; set; } = null!;
        public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;uid=root;password=1234;database=pos_db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.ProductCode, "product_code_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UnitId, "product_unit_id_idx");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Price)
                    .HasPrecision(11, 2)
                    .HasColumnName("price");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(45)
                    .HasColumnName("product_code");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(45)
                    .HasColumnName("product_name");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_unit_id");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("receipt");

                entity.HasIndex(e => e.ReceiptCode, "receipt_code_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.DiscountTotal)
                    .HasPrecision(11, 2)
                    .HasColumnName("discount_total");

                entity.Property(e => e.FullTotal)
                    .HasPrecision(11, 2)
                    .HasColumnName("full_total");

                entity.Property(e => e.GrandTotal)
                    .HasPrecision(11, 2)
                    .HasColumnName("grand_total");

                entity.Property(e => e.ReceiptCode)
                    .HasMaxLength(45)
                    .HasColumnName("receipt_code");

                entity.Property(e => e.SubTotal)
                    .HasPrecision(11, 2)
                    .HasColumnName("sub_total");
            });

            modelBuilder.Entity<ReceiptDetail>(entity =>
            {
                entity.HasKey(e => e.RdId)
                    .HasName("PRIMARY");

                entity.ToTable("receipt_detail");

                entity.HasIndex(e => e.ProductId, "receipt_detail_product_id_idx");

                entity.HasIndex(e => e.ReceiptId, "receipt_detail_rd_id_idx");

                entity.Property(e => e.RdId).HasColumnName("rd_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(11, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.DiscountPercent)
                    .HasPrecision(11, 2)
                    .HasColumnName("discount_percent");

                entity.Property(e => e.DiscountTotal)
                    .HasPrecision(11, 2)
                    .HasColumnName("discount_total");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");

                entity.Property(e => e.Total)
                    .HasPrecision(11, 2)
                    .HasColumnName("total");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ReceiptDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("receipt_detail_product_id");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ReceiptDetails)
                    .HasForeignKey(d => d.ReceiptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("receipt_detail_rd_id");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PRIMARY");

                entity.ToTable("unit");

                entity.HasIndex(e => e.UnitName, "unit_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .HasColumnName("unit_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
