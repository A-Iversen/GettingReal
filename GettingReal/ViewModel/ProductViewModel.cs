using GettingReal.Model;
using GettingReal.Repository;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace GettingReal.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        private List<Product> _products;
        private IProductRepository _repository;

        public List<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ProductViewModel()
        {
            // Create repository and load products
            _repository = new ProductRepository();
            Products = _repository.GetAllProducts();
        }

        // Add a new product
        public void AddProduct(string name, string category, int sku)
        {
            var product = new Product
            {
                Name = name,
                Category = category,
                SKU = sku,
                DateAcquired = System.DateTime.Now
            };
            
            _repository.Add(product);
            Products = _repository.GetAllProducts();
        }

        // Edit an existing product
        public void EditProduct(Product product)
        {
            _repository.Edit(product);
            Products = _repository.GetAllProducts();
        }

        // Delete a product
        public void DeleteProduct(Product product)
        {
            _repository.Delete(product);
            Products = _repository.GetAllProducts();
        }

        // Clear all products
        public void ClearProducts()
        {
            _repository.ClearProducts();
            Products = _repository.GetAllProducts();
        }

        public void SaveProducts()
        {
            // The repository automatically saves products when they are added, edited, or deleted
            // This method is kept for backward compatibility and can be used to force a save if needed
            Products = _repository.GetAllProducts();
        }

        private string ProductsFilePath
        {
            get
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string folder = Path.Combine(appDataPath, "GettingReal");
                Directory.CreateDirectory(folder); // Ensure the folder exists
                return Path.Combine(folder, "products.txt");
            }
        }
        public void SaveProductsToFile()
        {
            var sb = new StringBuilder();
            foreach (var product in Products)
            {
                sb.AppendLine($"{product.Name},{product.Category},{product.SKU}");
            }
            File.WriteAllText(ProductsFilePath, sb.ToString());
        }
    }
}