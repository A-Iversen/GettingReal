using FontAwesome.Sharp;
using GettingReal.Model;
using GettingReal.Repository;
using GettingReal.ViewModel;
using GettingReal.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace GettingReal;

using System.Collections.ObjectModel;
using GettingReal.Helpers;

/// <summary>
/// MainViewModel.cs h�ndterer logikken bag ProductView.xaml og indeholder alle properties og commands der bruges i Prow.xaml
/// -A
/// </summary>
public class MainViewModel : ViewModelBase
{
    //Fields og Properties for Product
    private string _productName;
    private double _length;
    private double _height;
    private double _width;
    private bool _fragile;
    private List<Product> _products;

    private readonly IProductRepository _repository;


    public ObservableCollection<Product> Products { get; set; } = new();

    public string ProductName
    {
        get => _productName;
        set
        {
            if (_productName != value)
            {
                _productName = value;
                OnPropertyChanged();
            }
        }
    }

    public double Length
    {
        get => _length;
        set
        {
            if (_length != value)
            {
                _length = value;
                OnPropertyChanged();
            }
        }
    }

    public double Height
    {
        get => _height;
        set
        {
            if (_height != value)
            {
                _height = value;
                OnPropertyChanged();
            }
        }
    }

    public double Width
    {
        get => _width;
        set
        {
            if (_width != value)
            {
                _width = value;
                OnPropertyChanged();
            }
        }
    }

    public bool Fragile
    {
        get => _fragile;
        set
        {
            if (_fragile != value)
            {
                _fragile = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsProductViewVisible => CurrentChildView is ProductViewModel;
    public bool IsPackagingViewVisible => CurrentChildView is PackagingViewModel;

    //Fields og Properties for viewChange
    private ViewModelBase _currentChildView;
    private string _caption;
    private IconChar _icon;

    public ViewModelBase CurrentChildView
    {
        get => _currentChildView;
        set
        {
            if (_currentChildView != value)
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
                OnPropertyChanged(nameof(IsProductViewVisible));
                OnPropertyChanged(nameof(IsPackagingViewVisible));
            }
        }
    }

    //Commands
    public ICommand ShowProductViewCommand { get; }
    public ICommand ShowPackagingViewCommand { get; }

    public MainViewModel()
    {
        //Initiliaze commands
        ShowProductViewCommand = new RelayCommand(ExecuteShowProductViewCommand);
        ShowPackagingViewCommand = new RelayCommand(ExecuteShowPackagingViewCommand);

        //Default View
        
    }

    private void ExecuteShowPackagingViewCommand(object obj)
    {
        CurrentChildView = new PackagingViewModel();
    }

    private void ExecuteShowProductViewCommand(object obj)
    {
        CurrentChildView = new ProductViewModel();
    }







   
    
    
}
