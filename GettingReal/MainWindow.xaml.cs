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

        //Kode til at flytte Title Bar og Resize vindue.
        //Stjæler Windows OS'ets indbyggede funktion til at tracke en application vindue resolution.
            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

            //Lader en dragge et vindue ved at snyde Windows til at tro at man dragger på default title baren, hvorimod et stackpanel bliver brugt istedet
            private void pnlControlBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            {
                WindowInteropHelper helper = new WindowInteropHelper(this);
                SendMessage(helper.Handle, 161,2,0);
            }

            //Vær gang at vinduet er resized nedsætter den max height så vinduet ikke overtager taskbaren for neden (Tænk på en fullscreen youtube video)
            private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
            {
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            }


        //Lukker Vindue
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //Minimizer vindue
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //Maximizer vindue
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
    }
}