using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GettingReal.Model;
using GettingReal.Repository;

namespace GettingReal.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IProductRepository _repository;
        private string _productName;
        private double _length;
        private double _height;
        private double _width;
        private bool _isFragile;
        private List<Product> _products;

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
        public MainViewModel()
        {
            _repository = new ProductRepository();
            Products = new List<Product>(_repository.GetAllProducts());
        }

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
} 