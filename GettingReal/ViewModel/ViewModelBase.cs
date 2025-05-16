using GettingReal.Model;
using GettingReal.Repository;
using GettingReal.View;
using GettingReal.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
namespace GettingReal;


public class ViewModelBase : INotifyPropertyChanged
{
    private readonly IProductRepository _repository;
    private string _productName;
    private double _length;
    private double _height;
    private double _width;
    private bool _isFragile;
    private List<Product> _products;

    //Properties for product(Tror jeg, i skal blive bedre til at kategorisere kode med comments >:( )
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



    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


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
    public ICommand ShowInventoryCommand { get; }
    public ICommand ShowPackagingCommand { get; }

}
