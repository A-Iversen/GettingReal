using GettingReal.Helpers;
using GettingReal.Model;
using GettingReal.ViewModel;
using System.Windows.Input;

public class PackagingViewModel : ViewModelBase
{
    public ViewModelBase CurrentChildView { get; set; }
    public ICommand ShowProductViewCommand { get; }
    public ICommand ShowPackagingViewCommand { get; }

    public List<Packaging> Packagings { get; set; }
    public string PackagingName { get; set; }

    public PackagingViewModel()
    {
        ShowProductViewCommand = new RelayCommand(ExecuteShowProductViewCommand);
        ShowPackagingViewCommand = new RelayCommand(ExecuteShowPackagingViewCommand);
    }

    private void ExecuteShowProductViewCommand(object obj)
    {
        
        CurrentChildView = new ProductViewModel();
        OnPropertyChanged(nameof(CurrentChildView));
    }

    private void ExecuteShowPackagingViewCommand(object obj)
    {
        
        CurrentChildView = new PackagingViewModel();
        OnPropertyChanged(nameof(CurrentChildView));
    }
}