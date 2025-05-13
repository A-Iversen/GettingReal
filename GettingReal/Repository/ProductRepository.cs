using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GettingReal.Model;

namespace GettingReal.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _filePath;
        private List<Product> _products;

        // Constructor - initializes the repository with a file path
        public ProductRepository()
        {
            // Use a simple, fixed path in the user's documents folder
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _filePath = Path.Combine(documentsPath, "GettingReal", "products.txt");
            
            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
            
            _products = new List<Product>();
            LoadProducts();
        }

        // Load products from file
        private void LoadProducts()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    _products = File.ReadAllLines(_filePath)
                        .Select(Product.FromString)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                Console.WriteLine($"Error loading products: {ex.Message}");
                _products = new List<Product>();
            }
        }

        // Save products to file
        private void SaveProducts()
        {
            try
            {
                File.WriteAllLines(_filePath, _products.Select(p => p.ToString()));
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                Console.WriteLine($"Error saving products: {ex.Message}");
            }
        }

        // IProductRepository
        public void AddProduct(Product product)
        {
            _products.Add(product);
            SaveProducts();
        }

        public List<Product> GetAllProducts()
        {
            return _products.ToList();
        }

        public void ClearProducts()
        {
            _products.Clear();
            SaveProducts();
        }
    }
} 