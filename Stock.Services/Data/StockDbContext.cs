using Microsoft.EntityFrameworkCore;
using Stock.Entities.Entities;
using System.Reflection.Emit;

namespace Stock.Services.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(
            DbContextOptions<StockDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductGroup> ProductGroups { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<ProductGroup>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<User>()
                .ToTable("user");

            modelBuilder.Entity<User>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
