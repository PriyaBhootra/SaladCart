using SaladCart.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace SaladCart.Data
{
    public class DbSeeder
    {


        #region private methods

       

        private static async Task SeedOrderStatusAsync(ApplicationDbContext context)
        {
            var orderStatuses = new[]
            {
            new OrderStatus { StatusId = 1, StatusName = "Pending" },
            new OrderStatus { StatusId = 2, StatusName = "Shipped" },
            new OrderStatus { StatusId = 3, StatusName = "Delivered" },
            new OrderStatus { StatusId = 4, StatusName = "Cancelled" },
            new OrderStatus { StatusId = 5, StatusName = "Returned" },
            new OrderStatus { StatusId = 6, StatusName = "Refund" }
            };

            await context.orderStatuses.AddRangeAsync(orderStatuses);
            await context.SaveChangesAsync();
        }

        private static async Task SeedBooksAsync(ApplicationDbContext context)
        {
            var salads = new List<Salads>
            {
            // Indian Salads (Type = Indian)
            new Salads { Name = "Paneer Tikka Salad",Description="Paneer Tikka Salad", Type = "Indian", Price = 200},
            new Salads { Name = "Sprouts Chat Salad",Description="Sprouts Chat Salad", Type = "Indian", Price = 180},
            new Salads { Name = "Moong Dal Kosambari", Description="Moong Dal Kosambari", Type = "Indian", Price = 180},
            new Salads { Name = "Soya Chunk Salad", Description="Soya Chunk Salad", Type = "Indian", Price = 180},
            new Salads { Name = "Aloo Chana Chat Salad", Description="Aloo Chana Chat Salad", Type = "Indian", Price = 180},
            
            // Continental Salads (Type = Continental)
            new Salads { Name = "Beetroot Orange Salad", Description="Beetroot Orange Salad", Type = "Continental", Price = 200},
            new Salads { Name = "Detox Thai Rainbow Salad", Description="Detox Thai Rainbow Salad", Type = "Continental", Price = 200},
            new Salads { Name = "Quinoa Corn Salad", Description="Quinoa Corn Salad", Type = "Continental", Price = 200},
            new Salads { Name = "Stir-Fry Veggis Salad", Description="Stir-Fry Veggis Salad",  Type = "Continental", Price = 200},
            new Salads { Name = "Pesto Pasta Salad", Description="Pesto Pasta Salad",  Type = "Continental", Price = 200},

                // Fruits Salads (Type = Fruits)
            new Salads { Name = "Mango Thai Salad", Description="Mango Thai Salad", Type = "Fruits", Price = 200},
            new Salads { Name = "Tropical Fruit  Salad", Description="Tropical Fruit  Salad", Type = "Fruits", Price = 200},
            new Salads { Name = "Apple Cucumber Yogurt Salad", Description="Apple Cucumber Yogurt Salad",  Type = "Fruits", Price = 200},
            new Salads { Name = "Watermelon Feta  Salad", Description="Watermelon Feta  Salad", Type = "Fruits", Price = 200},
            new Salads { Name = "Seasonal Fruit Chat", Description="Seasonal Fruit Chat", Type = "Fruits", Price = 200}

             };

            await context.Salads.AddRangeAsync(salads);
            await context.SaveChangesAsync();
        }

       #endregion

    }
}
