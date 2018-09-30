using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOProject
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext()
        {
        }

        public ExchangeContext(DbContextOptions<ExchangeContext> options) : base(options)
        {
        }

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<Trade> Trades { get; set; }

        public DbSet<Share> Shares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });
            
            modelBuilder.Entity<Share>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.HasIndex(e => e.Symbol).HasName("Share_Symbol_Index").IsUnique();

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Ignore(p => p.CurrentPrice);
            });

            modelBuilder.Entity<ShareRates>(entity =>
            {
                entity.HasKey(e => new { e.ShareId, e.TimeStamp });

                entity.HasOne<Share>()
                    .WithMany(p => p.Rates)
                    .HasForeignKey(e => e.ShareId)
                    .HasConstraintName("FK_Share_Rate");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.SinglePrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.TotalPrice)
                    .HasComputedColumnSql("[SinglePrice]*[Quantity]")
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasConversion(v => v.ToString(), v => (OperationEnum)Enum.Parse(typeof(OperationEnum), v));
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
