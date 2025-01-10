using ConsoleApp10.Interfaces;
using ConsoleApp10.Models;

namespace ConsoleApp10.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public void PlaceOrder(Order order)
        {
            var product = _productRepository.GetProductById(order.ProductId);
            if (product == null || product.Stock < order.Quantity)
            {
                throw new InvalidOperationException("Insufficient stock.");
            }

            product.Stock -= order.Quantity;
            _productRepository.UpdateProduct(product);
            _orderRepository.PlaceOrder(order);
        }
    }
}
