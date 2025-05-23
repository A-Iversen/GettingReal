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
            // Save File
string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
_filePath = Path.Combine(documentsPath, "GettingReal", "products.json");
            
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

        public void EditProduct(string productName, Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Name == productName);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Length = updatedProduct.Length;
                product.Height = updatedProduct.Height;
                product.Width = updatedProduct.Width;
                product.IsFragile = updatedProduct.IsFragile;
                SaveProducts();
            }
        }

        public void RemoveProduct(string productName)
        {
            var product = _products.FirstOrDefault(p => p.Name == productName);
            if (product != null)
            {
                _products.Remove(product);
                SaveProducts();
            }
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Edit(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
