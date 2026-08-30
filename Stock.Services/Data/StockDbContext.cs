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

        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("user_profiles", schema: "auth").HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("user_profile_id").IsRequired().UseIdentityColumn().ValueGeneratedOnAdd(); 
                entity.Property(e => e.Name).HasColumnName("user_profile_name").IsRequired(); 
            });


            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.ToTable("product_groups", schema: "stockgeneral").HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("product_group_id").IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("product_group_name").IsRequired();
            });



            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", schema: "stockgeneral").HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("product_id").IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("product_name").IsRequired();
                entity.Property(e => e.Description).HasColumnName("product_description").IsRequired();
                entity.Property(e => e.ProductGroupId).HasColumnName("product_group_id").IsRequired();

                entity.HasOne(p => p.ProductGroup)
        .WithMany()
        .HasForeignKey(p => p.ProductGroupId);
            });

            modelBuilder.Entity<User>()
                .ToTable("user", schema: "auth"); // şema doğruysa

            modelBuilder.Entity<User>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}

