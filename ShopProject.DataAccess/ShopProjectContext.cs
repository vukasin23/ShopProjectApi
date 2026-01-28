using Microsoft.EntityFrameworkCore;
using ShopProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.DataAccess
{
    public class ShopProjectContext:DbContext
    {
        private readonly string _connectionString;
        public ShopProjectContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ShopProjectContext()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopProject;Integrated Security=True";
        }
        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UseCases)
                .WithMany(uc => uc.Users)
                .UsingEntity(j => j.ToTable("UserUseCases"));

            modelBuilder.Entity<Category>()
                    .HasOne(c => c.Parent)
                    .WithMany(c => c.Children)
                    .HasForeignKey(c => c.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                    .HasOne(c => c.User)
                    .WithOne(u => u.Cart)
                    .HasForeignKey<Cart>(c => c.UserId)
                     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart>()
                    .HasIndex(c => c.UserId)
                    .IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<WishlistItem> WishlistItems{ get; set; }
        public DbSet<ShippingMethod> ShippingMethods{ get; set; }
        public DbSet<Address> Addresses { get; set; }
        }
}
