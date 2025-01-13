using ConsoleApp10.Models;
using ConsoleApp10.Interfaces;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ConsoleApp10.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void PlaceOrder(Order order)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = "INSERT INTO Orders (ProductId, Quantity, OrderDate) VALUES (@ProductId, @Quantity, @OrderDate)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductId", order.ProductId);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.ExecuteNonQuery();
            }

            var updateStockQuery = "UPDATE Products SET Stock = Stock - @Quantity WHERE ProductId = @ProductId";
            using var updateCommand = new SqlCommand(updateStockQuery, connection);
            updateCommand.Parameters.AddWithValue("@Quantity", order.Quantity);
            updateCommand.Parameters.AddWithValue("@ProductId", order.ProductId);
            updateCommand.ExecuteNonQuery();
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT OrderId, ProductId, Quantity, OrderDate FROM Orders";
                using var command = new SqlCommand(query, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        OrderId = (int)reader["OrderId"],
                        ProductId = (int)reader["ProductId"],
                        Quantity = (int)reader["Quantity"],
                        OrderDate = (DateTime)reader["OrderDate"]
                    });
                }
            }
            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT OrderId, ProductId, Quantity, OrderDate FROM Orders WHERE OrderId = @OrderId";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@OrderId", orderId);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Order
                {
                    OrderId = (int)reader["OrderId"],
                    ProductId = (int)reader["ProductId"],
                    Quantity = (int)reader["Quantity"],
                    OrderDate = (DateTime)reader["OrderDate"]
                };
            }
            return null;
        }
    }
}
