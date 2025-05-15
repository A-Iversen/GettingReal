using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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
                    string json = File.ReadAllText(_filePath);
                    _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                _products = new List<Product>();
            }
        }


        // Save products to file
        private void SaveProducts()
        {
            try
            {
                string json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
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