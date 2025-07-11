
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaladCart.Models;

namespace SaladCart.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Salads> Salads { get; set; }       
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> orderStatuses { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Salads>().HasData
                (
                    new Salads { Id = 1, Name = "Paneer Tikka Salad", Description = "Paneer Tikka Salad", Type = "Indian", Price = 200 },
                    new Salads { Id = 2, Name = "Sprouts Chat Salad", Description = "Sprouts Chat Salad", Type = "Indian", Price = 180 },
                    new Salads { Id = 3, Name = "Moong Dal Kosambari", Description = "Moong Dal Kosambari", Type = "Indian", Price = 180 },
                    new Salads { Id = 4, Name = "Soya Chunk Salad", Description = "Soya Chunk Salad", Type = "Indian", Price = 180 },
                    new Salads { Id = 5, Name = "Aloo Chana Chat Salad", Description = "Aloo Chana Chat Salad", Type = "Indian", Price = 180 },

                    // Continental Salads (Type = Continental)
                    new Salads { Id = 6, Name = "Beetroot Orange Salad", Description = "Beetroot Orange Salad", Type = "Continental", Price = 200 },
                    new Salads { Id = 7, Name = "Detox Thai Rainbow Salad", Description = "Detox Thai Rainbow Salad", Type = "Continental", Price = 200 },
                    new Salads { Id = 8, Name = "Quinoa Corn Salad", Description = "Quinoa Corn Salad", Type = "Continental", Price = 200 },
                    new Salads { Id = 9, Name = "Stir-Fry Veggis Salad", Description = "Stir-Fry Veggis Salad", Type = "Continental", Price = 200 },
                    new Salads { Id = 10, Name = "Pesto Pasta Salad", Description = "Pesto Pasta Salad", Type = "Continental", Price = 200 },

                    // Fruits Salads (Type = Fruits)
                    new Salads { Id = 11, Name = "Mango Thai Salad", Description = "Mango Thai Salad", Type = "Fruits", Price = 200 },
                    new Salads { Id = 12, Name = "Tropical Fruit  Salad", Description = "Tropical Fruit  Salad", Type = "Fruits", Price = 200 },
                    new Salads { Id = 13, Name = "Apple Cucumber Yogurt Salad", Description = "Apple Cucumber Yogurt Salad", Type = "Fruits", Price = 200 },
                    new Salads { Id = 14, Name = "Watermelon Feta  Salad", Description = "Watermelon Feta  Salad", Type = "Fruits", Price = 200 },
                    new Salads { Id = 15, Name = "Seasonal Fruit Chat", Description = "Seasonal Fruit Chat", Type = "Fruits", Price = 200 }
                );

            modelBuilder.Entity<OrderStatus>().HasData
                (
                    new OrderStatus { Id = 1, StatusId = 1, StatusName = "Pending" },
                    new OrderStatus { Id = 2,StatusId = 2, StatusName = "Shipped" },
                    new OrderStatus { Id = 3, StatusId = 3, StatusName = "Delivered" },
                    new OrderStatus { Id = 4, StatusId = 4, StatusName = "Cancelled" },
                    new OrderStatus { Id = 5, StatusId = 5, StatusName = "Returned" },
                    new OrderStatus { Id = 6, StatusId = 6, StatusName = "Refund" }
                );
        }
    }
}
