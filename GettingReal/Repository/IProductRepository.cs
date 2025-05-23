using System.Collections.Generic;
using GettingReal.Model;

namespace GettingReal.Repository
{
    public interface IProductRepository
    {
        void Add(Product product);

        void Edit(Product product);

        void Delete(Product product);


        // Get all products
        List<Product> GetAllProducts();

        // Clear all products
        void ClearProducts();
    }
} 