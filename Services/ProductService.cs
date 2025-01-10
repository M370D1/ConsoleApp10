using ConsoleApp10.Interfaces;
using ConsoleApp10.Models;
using System.Collections.Generic;

namespace ConsoleApp10.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Add a new product
        public void AddProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            if (product.Price <= 0)
            {
                throw new ArgumentException("Product price must be greater than zero.");
            }

            if (product.Stock < 0)
            {
                throw new ArgumentException("Stock cannot be negative.");
            }

            _productRepository.AddProduct(product);
        }

        // Get all products
        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        // Get a product by ID
        public Product GetProductById(int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            return product;
        }

        // Update an existing product
        public void UpdateProduct(Product product)
        {
            var existingProduct = _productRepository.GetProductById(product.ProductId);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.ProductId} not found.");
            }

            if (product.Price <= 0)
            {
                throw new ArgumentException("Product price must be greater than zero.");
            }

            if (product.Stock < 0)
            {
                throw new ArgumentException("Stock cannot be negative.");
            }

            _productRepository.UpdateProduct(product);
        }

        // Delete a product by ID
        public void DeleteProduct(int productId)
        {
            var existingProduct = _productRepository.GetProductById(productId);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            _productRepository.DeleteProduct(productId);
        }
    }
}
