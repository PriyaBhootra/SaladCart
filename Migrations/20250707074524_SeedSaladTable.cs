using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SaladCart.Migrations
{
    /// <inheritdoc />
    public partial class SeedSaladTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, 1, "Pending" },
                    { 2, 2, "Shipped" },
                    { 3, 3, "Delivered" },
                    { 4, 4, "Cancelled" },
                    { 5, 5, "Returned" },
                    { 6, 6, "Refund" }
                });

            migrationBuilder.InsertData(
                table: "Salads",
                columns: new[] { "Id", "Description", "Image", "Name", "Price", "Quantity", "Type" },
                values: new object[,]
                {
                    { 1, "Paneer Tikka Salad", null, "Paneer Tikka Salad", 200.0, 0, "Indian" },
                    { 2, "Sprouts Chat Salad", null, "Sprouts Chat Salad", 180.0, 0, "Indian" },
                    { 3, "Moong Dal Kosambari", null, "Moong Dal Kosambari", 180.0, 0, "Indian" },
                    { 4, "Soya Chunk Salad", null, "Soya Chunk Salad", 180.0, 0, "Indian" },
                    { 5, "Aloo Chana Chat Salad", null, "Aloo Chana Chat Salad", 180.0, 0, "Indian" },
                    { 6, "Beetroot Orange Salad", null, "Beetroot Orange Salad", 200.0, 0, "Continental" },
                    { 7, "Detox Thai Rainbow Salad", null, "Detox Thai Rainbow Salad", 200.0, 0, "Continental" },
                    { 8, "Quinoa Corn Salad", null, "Quinoa Corn Salad", 200.0, 0, "Continental" },
                    { 9, "Stir-Fry Veggis Salad", null, "Stir-Fry Veggis Salad", 200.0, 0, "Continental" },
                    { 10, "Pesto Pasta Salad", null, "Pesto Pasta Salad", 200.0, 0, "Continental" },
                    { 11, "Mango Thai Salad", null, "Mango Thai Salad", 200.0, 0, "Fruits" },
                    { 12, "Tropical Fruit  Salad", null, "Tropical Fruit  Salad", 200.0, 0, "Fruits" },
                    { 13, "Apple Cucumber Yogurt Salad", null, "Apple Cucumber Yogurt Salad", 200.0, 0, "Fruits" },
                    { 14, "Watermelon Feta  Salad", null, "Watermelon Feta  Salad", 200.0, 0, "Fruits" },
                    { 15, "Seasonal Fruit Chat", null, "Seasonal Fruit Chat", 200.0, 0, "Fruits" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Salads",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
