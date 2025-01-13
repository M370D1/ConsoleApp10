using ConsoleApp10.Models;
using ConsoleApp10.Repositories;
using ConsoleApp10.Services;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConnectionConfig.DefaultConnection;

            var productRepository = new ProductRepository(connectionString);
            var orderRepository = new OrderRepository(connectionString);

            var productService = new ProductService(productRepository);
            var orderService = new OrderService(orderRepository, productRepository);

            // Example: Adding a product
            var newProduct = new Product { Name = "Tablet", Price = 499.99m, Stock = 20 };
            var newPeoduct2 = new Product { Name = "PlayStation5", Price = 1400m, Stock = 1 };
            productService.AddProduct(newProduct);
            productService.AddProduct(newPeoduct2);


            // Example: Placing an order
            var newOrder = new Order { ProductId = 1, Quantity = 2, OrderDate = DateTime.Now };
            orderService.PlaceOrder(newOrder);

            // Display all products
            var products = productService.GetAllProducts();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductId}: {p.Name} - {p.Price:C} - {p.Stock} left");
            }
        }
    }
}
