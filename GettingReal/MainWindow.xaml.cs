using System.Windows;
using GettingReal.Repository;
using GettingReal.ViewModel;
using GettingReal.View;

namespace GettingReal
{
    public partial class MainWindow : Window
    {
        private MainViewModel _view;

        public MainWindow()
        {
            InitializeComponent();
            _view = new MainViewModel(new ProductRepository());
            DataContext = _view;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _view.CurrentView = new PackagingView();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _view.CurrentView = new ProductView();
        }
    }
}