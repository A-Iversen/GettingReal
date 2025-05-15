using System.Windows;
using GettingReal.Repository;
using GettingReal.ViewModel;
using GettingReal.View;
using System.Runtime.InteropServices;
using System.Windows.Interop;

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
        
        
        //Lader en dragge vinduet rundt når man holder venstre museknap nede
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161,2,0);
        }

        private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
    }
}