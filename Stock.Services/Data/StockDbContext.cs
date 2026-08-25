using Microsoft.EntityFrameworkCore;
using Stock.Entities.Entities;

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

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.ToTable("product_groups", schema: "stockgeneral").HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("product_group_id").IsRequired();
                entity.Property(e => e.Name).HasColumnName("product_group_name").IsRequired(); 
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", schema: "stockgeneral");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<User>()
                .ToTable("user", schema: "auth"); // şema doğruysa

            modelBuilder.Entity<User>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            base.OnModelCreating(modelBuilder);
        }

    
    }
}
