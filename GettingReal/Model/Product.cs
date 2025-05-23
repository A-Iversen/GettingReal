using GettingReal.Helpers;
using System;

namespace GettingReal.Model
{
    public class Product : ObservableObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; onPropertyChanged(); }
        }

        private double _length;
        public double Length
        {
            get => _length;
            set { _length = value; onPropertyChanged(); }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set { _height = value; onPropertyChanged(); }
        }

        private double _width;
        public double Width
        {
            get => _width;
            set { _width = value; onPropertyChanged(); }
        }

        private bool _fragile;
        public bool Fragile
        {
            get => _fragile;
            set { _fragile = value; onPropertyChanged(); }
        }
    }
} 