using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GettingReal.Model;

namespace GettingReal.Repository
{
    public class ProductRepository : IProductRepository
    {
        private string _filePath;
        private List<Product> _products;

        public ProductRepository()
        {
            // Save file in Documents folder
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _filePath = Path.Combine(documentsPath, "GettingReal", "products.json");
            
            Console.WriteLine($"File path: {_filePath}");
            
            // Create folder if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
            
            // Initialize empty list
            _products = new List<Product>();
            
            // Load existing products
            LoadProducts();
        }

        private void LoadProducts()
        {
            if (File.Exists(_filePath))
            {
                Console.WriteLine("File exists, trying to load products");
                string json = File.ReadAllText(_filePath);
                Console.WriteLine($"Loaded JSON: {json}");
                
                if (!string.IsNullOrEmpty(json))
                {
                    _products = JsonSerializer.Deserialize<List<Product>>(json);
                    Console.WriteLine($"Loaded {_products.Count} products");
                }
                else
                {
                    Console.WriteLine("File is empty");
                    _products = new List<Product>();
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
                _products = new List<Product>();
            }
        }

        private void SaveProducts()
        {
            string json = JsonSerializer.Serialize(_products);
            Console.WriteLine($"Saving JSON: {json}");
            File.WriteAllText(_filePath, json);
        }

        public void Add(Product product)
        {
            _products.Add(product);
            SaveProducts();
        }

        public void Edit(Product product)
        {
            // Find the product by name
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].Name == product.Name)
                {
                    _products[i] = product;
                    SaveProducts();
                    break;
                }
            }
        }

        public void Delete(Product product)
        {
            _products.Remove(product);
            SaveProducts();
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public void ClearProducts()
        {
            _products.Clear();
            SaveProducts();
        }
    }
}
