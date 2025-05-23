using System.Windows;
using GettingReal.Model;

namespace GettingReal.View
{
    public partial class ProductDialog : Window
    {
        public Product Product { get; private set; }

        public ProductDialog()
        {
            InitializeComponent();
            Product = new Product();
            DataContext = Product;
        }

        public ProductDialog(Product productToEdit)
        {
            InitializeComponent();
            Product = new Product
            {
                Name = productToEdit.Name,
                Category = productToEdit.Category,
                SKU = productToEdit.SKU
                // Copy other properties as needed
            };
            DataContext = Product;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}