using GettingReal.Model;
using GettingReal.Repository;
using GettingReal.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace GettingReal.ViewModel
/// <summary>
/// ViewModelBase.cs implenterer INotifyPropertyChanged OG håndterer logikken mellem viewmodels. 
/// Bruges IKKE til at kommunikere direkte med hverken Model eller View
/// -A
/// </summary>
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
