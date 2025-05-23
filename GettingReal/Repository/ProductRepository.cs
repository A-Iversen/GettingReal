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
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _filePath = Path.Combine(appDataPath, "GettingReal", "products.txt");
            
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
                    var lines = File.ReadAllLines(_filePath);
                    _products = lines.Select(line =>
                    {
                        var parts = line.Split(',');
                        return new Product
                        {
                            Name = parts[0],
                            Length = double.Parse(parts[1]),
                            Height = double.Parse(parts[2]),
                            Width = double.Parse(parts[3]),
                            Fragile = bool.Parse(parts[4])
                        };
                    }).ToList();
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
                var lines = _products.Select(p => $"{p.Name},{p.Length},{p.Height},{p.Width},{p.Fragile}");
                File.WriteAllLines(_filePath, lines);
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
                product.Fragile = updatedProduct.Fragile;
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
