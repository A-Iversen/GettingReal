using GettingReal.Model;
using GettingReal.Repository;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GettingReal.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        private List<Product> _products;
        public List<Product> Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }

        private readonly IProductRepository _repository;

        public ProductViewModel()
        {
            _repository = new ProductRepository(); // Or inject via constructor
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