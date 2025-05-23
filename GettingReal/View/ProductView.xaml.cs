using System.Windows;
using GettingReal.Repository;
using GettingReal.ViewModel;
using GettingReal.View;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Controls;
using GettingReal.Model;
using GettingReal.Helpers;
using System.Collections.Generic;

namespace GettingReal.View
{
    public partial class ProductView : Window
    {
        private MainViewModel _view;
        private readonly ProductViewModel _productViewModel;

        //Kode til at flytte Title Bar og Resize vindue.
        //Stjæler Windows OS'ets indbyggede funktion til at tracke en application vindue resolution.
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        //Lader en dragge et vindue ved at snyde Windows til at tro at man dragger på default title baren, hvorimod et stackpanel bliver brugt istedet
        private void pnlControlBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        //Vær gang at vinduet er resized nedsætter den max height så vinduet ikke overtager taskbaren for neden (Tænk på en fullscreen youtube video)
        private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }


        //Lukker Vindue
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //Minimizer vindue
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //Maximizer vindue
        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        public ProductView()
        {
            InitializeComponent();
            _view = new MainViewModel();
            _productViewModel = new ProductViewModel();
            DataContext = _view;
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialog();
            if (dialog.ShowDialog() == true)
            {
                if (DataContext is MainViewModel vm && dialog.Product != null)
                {
                    vm.Products.Add(dialog.Product);
                    _productViewModel.Products = new List<Product>(vm.Products);
                    _productViewModel.SaveProductsToFile();
                }
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListView.SelectedItem is Product selectedProduct)
            {
                var dialog = new ProductDialog(selectedProduct);
                if (dialog.ShowDialog() == true && dialog.Product != null)
                {
                    // Update the selected product's properties
                    selectedProduct.Name = dialog.Product.Name;
                    selectedProduct.Length = dialog.Product.Length;
                    selectedProduct.Height = dialog.Product.Height;
                    selectedProduct.Width = dialog.Product.Width;
                    selectedProduct.Fragile = dialog.Product.Fragile;
                    
                    // Save changes
                    if (DataContext is MainViewModel vm)
                    {
                        _productViewModel.Products = new List<Product>(vm.Products);
                        _productViewModel.SaveProductsToFile();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to edit.");
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListView.SelectedItem is Product selectedProduct)
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{selectedProduct.Name}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (DataContext is MainViewModel vm)
                    {
                        vm.Products.Remove(selectedProduct);
                        _productViewModel.Products = new List<Product>(vm.Products);
                        _productViewModel.SaveProductsToFile();
                    }
                }
            }
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}