using System;
using System.Collections.Generic;
using System.Text;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Order> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            // Restrict deletion of related order when OrderProducts entry is removed
            modelBuilder.Entity<Order> ()
                .HasMany (o => o.OrderProducts)
                .WithOne (l => l.Order)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Product> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            // Restrict deletion of related product when OrderProducts entry is removed
            modelBuilder.Entity<Product> ()
                .HasMany (o => o.OrderProducts)
                .WithOne (l => l.Product)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentType> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            ApplicationUser user = new ApplicationUser {
                FirstName = "admin",
                LastName = "admin",
                StreetAddress = "123 Infinity Way",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid ().ToString ("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser> ();
            user.PasswordHash = passwordHash.HashPassword (user, "Admin8*");
            modelBuilder.Entity<ApplicationUser> ().HasData (user);

            modelBuilder.Entity<PaymentType> ().HasData (
                new PaymentType () {
                    PaymentTypeId = 1,
                        UserId = user.Id,
                        Description = "American Express",
                        AccountNumber = "86753095551212"
                },
                new PaymentType () {
                    PaymentTypeId = 2,
                        UserId = user.Id,
                        Description = "Discover",
                        AccountNumber = "4102948572991"
                }
            );

            modelBuilder.Entity<ProductType> ().HasData (
                new ProductType () {
                    ProductTypeId = 1,
                        Label = "Sporting Goods"
                },
                new ProductType () {
                    ProductTypeId = 2,
                        Label = "Appliances"
                }
            );

            modelBuilder.Entity<Product> ().HasData (
                new Product () {
                    ProductId = 1,
                        ProductTypeId = 1,
                        UserId = user.Id,
                        Description = "It flies high",
                        Title = "Kite",
                        Quantity = 100,
                        Price = 2.99
                },
                new Product()
                {
                    ProductId = 3,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "It rolls flat",
                    Title = "Roller Blade",
                    Quantity = 0,
                    Price = 50.00
                },
                new Product ()
                {
                    ProductId = 2,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "It rolls fast",
                    Title = "Wheelbarrow",
                    Quantity = 5,
                    Price = 29.99
                },
                new Product()
                {
                    ProductId = 4,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "Cools my jello",
                    Title = "Refrigerator",
                    Quantity = 10,
                    Price = 500.00
                },
                new Product()
                {
                    ProductId = 5,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Baseball",
                    Title = "Baseball",
                    Quantity = 0,
                    Price = 10.00
                },
                new Product()
                {
                    ProductId = 5,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "Cleans my clothes",
                    Title = "Washer",
                    Quantity = 15,
                    Price = 100.00
                },
                new Product()
                {
                    ProductId = 6,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Ninja stuff",
                    Title = "Nunchucks",
                    Quantity = 0,
                    Price = 5.50
                },
                new Product()
                {
                    ProductId = 7,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "Cooks my turkey",
                    Title = "Stove",
                    Quantity = 0,
                    Price = 75.00
                },
                new Product()
                {
                    ProductId = 8,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "To hit baseballs",
                    Title = "Baseball Bat",
                    Quantity = 20,
                    Price = 40.00
                },
                new Product()
                {
                    ProductId = 9,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Takes your cat for a spin",
                    Title = "Dryer",
                    Quantity = 5,
                    Price = 120.00
                },
                new Product()
                {
                    ProductId = 10,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "To catch fish",
                    Title = "Fishing Rod",
                    Quantity = 100,
                    Price = 50.00
                },
                new Product()
                {
                    ProductId = 11,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Make decorative blocks of steel",
                    Title = "Trash Compactor",
                    Quantity = 150,
                    Price = 5000.00
                },
                new Product()
                {
                    ProductId = 12,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Beauty for the home",
                    Title = "Bubbles",
                    Quantity = 3,
                    Price = 0.50
                },
                new Product()
                {
                    ProductId = 13,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "To add drama to any room",
                    Title = "Fog machine",
                    Quantity = 2000,
                    Price = 25.00
                },
                new Product()
                {
                    ProductId = 14,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "To sail on water",
                    Title = "Boat",
                    Quantity = 10,
                    Price = 500.00
                },
                new Product()
                {
                    ProductId = 15,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "For all your sporting needs",
                    Title = "Sports bag",
                    Quantity = 0,
                    Price = 99.00
                },
                new Product()
                {
                    ProductId = 16,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "Make you super cool",
                    Title = "Air Conditioner",
                    Quantity = 1,
                    Price = 50000000.00
                },
                new Product()
                {
                    ProductId = 17,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "To make friends",
                    Title = "Dog",
                    Quantity = 85,
                    Price = 9832479820.00
                },
                new Product()
                {
                    ProductId = 18,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "Gets shit done",
                    Title = "Steam Roller",
                    Quantity = 18,
                    Price = 560.00
                },
                new Product()
                {
                    ProductId = 19,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Causes good feels",
                    Title = "Massager",
                    Quantity = 2,
                    Price = 8.00
                },
                new Product()
                {
                    ProductId = 20,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "Blows, literally",
                    Title = "Hair dryer",
                    Quantity = 0,
                    Price = 50.00
                },
                new Product()
                {
                    ProductId = 21,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Makes your dad fun again",
                    Title = "Football",
                    Quantity = 2,
                    Price = 10.00
                },
                new Product()
                {
                    ProductId = 22,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Your mom",
                    Title = "Back Brace",
                    Quantity = 0,
                    Price = 900.00
                }
            );

            modelBuilder.Entity<Order> ().HasData (
                new Order () {
                    OrderId = 1,
                    UserId = user.Id,
                    PaymentTypeId = null
                }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    OrderId = 2,
                    UserId = user.Id,
                    PaymentTypeId = null
                }
            );

            modelBuilder.Entity<OrderProduct> ().HasData (
                new OrderProduct () {
                    OrderProductId = 1,
                    OrderId = 1,
                    ProductId = 1
                }
            );

            modelBuilder.Entity<OrderProduct> ().HasData (
                new OrderProduct () {
                    OrderProductId = 2,
                    OrderId = 1,
                    ProductId = 2
                }
            );

            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct()
                {
                    OrderProductId = 3,
                    OrderId =2,
                    ProductId = 2
                }
            );

        }
    }
}