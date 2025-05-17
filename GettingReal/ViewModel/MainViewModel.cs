using FontAwesome.Sharp;
using GettingReal.Model;
using GettingReal.Repository;
using GettingReal.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace GettingReal;


public class MainViewModel : ViewModelBase
{
    //Fields og Properties for Product
    private string _productName;
    private double _length;
    private double _height;
    private double _width;
    private bool _isFragile;
    private List<Product> _products;

    private readonly IProductRepository _repository;

    public List<Product> Products
    {
        get => _products;
        private set
        {
            _products = value;
            OnPropertyChanged();
        }
    }

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

    public bool IsFragile
    {
        get => _isFragile;
        set
        {
            if (_isFragile != value)
            {
                _isFragile = value;
                OnPropertyChanged();
            }
        }
    }

//Fields og Properties for viewChange
    private ViewModelBase _currentChildView;
    private string _caption;
    private IconChar _icon;

    public ViewModelBase CurrentChildView
    {
        get => _currentChildView;
        set
        {
            _currentChildView = value;
            OnPropertyChanged(nameof(CurrentChildView));
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
        ExecuteShowPackagingViewCommand(null);
    }

    private void ExecuteShowPackagingViewCommand(object obj)
    {
        CurrentChildView = new PackagingViewModel();
    }

    private void ExecuteShowProductViewCommand(object obj)
    {
        CurrentChildView = new ProductViewModel();
    }







    //Det her skal puttes over i en PackagingViewModel >:(

    // Public parameterless constructor
    public void SaveProduct()
    {
        var product = new Product
        {
            Name = ProductName,
            Length = Length,
            Height = Height,
            Width = Width,
            IsFragile = IsFragile
        };

        _repository.AddProduct(product);
        Products = new List<Product>(_repository.GetAllProducts());
        ClearInputFields();
    }

    private void ClearInputFields()
    {
        ProductName = string.Empty;
        Length = 0;
        Height = 0;
        Width = 0;
        IsFragile = false;
    }
    
    
}
