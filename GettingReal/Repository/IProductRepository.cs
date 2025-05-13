using System.Collections.Generic;
using GettingReal.Model;

namespace GettingReal.Repository
{
    public interface IProductRepository
    {
        // Add a new product
        void AddProduct(Product product);

        // Get all products
        List<Product> GetAllProducts();

        // Clear all products
        void ClearProducts();
    }
} 