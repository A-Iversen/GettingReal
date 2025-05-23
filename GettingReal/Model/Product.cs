using GettingReal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Repeat for other properties...
        // Example:
        private string _category;
        public string Category
        {
            get => _category;
            set { _category = value; onPropertyChanged(); }
        }

        private int _sku;
        public int SKU
        {
            get => _sku;
            set { _sku = value; onPropertyChanged(); }
        }

        private string _storageStatus;
        public string StorageStatus
        {
            get => _storageStatus;
            set { _storageStatus = value; onPropertyChanged(); }
        }

        private DateTime _dateAcquired;
        public DateTime DateAcquired
        {
            get => _dateAcquired;
            set { _dateAcquired = value; onPropertyChanged(); }
        }

        private double _salePrice;
        public double SalePrice
        {
            get => _salePrice;
            set { _salePrice = value; onPropertyChanged(); }
        }

        private double _importCost;
        public double ImportCost
        {
            get => _importCost;
            set { _importCost = value; onPropertyChanged(); }
        }

        private double _length;
        public double Length
        {
            get => _length;
            set { _length = value; onPropertyChanged(); }
        }

        private double _width;
        public double Width
        {
            get => _width;
            set { _width = value; onPropertyChanged(); }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set { _height = value; onPropertyChanged(); }
        }

        // Tilføjet af PU mht Packaging
        /// <summary>
        /// Fragility buffer in cm (e.g. 0.5 or 1.0)
        /// </summary>
        public double FragilityBuffer { get; set; }

        // Tilføjet af PU mht Packaging
        /// <summary>
        /// Total dimensions including buffer on each side
        /// </summary>
        /// 
        public (double L, double H, double W) DimensionsWithBuffer
            => (Length + FragilityBuffer * 2,
                Height + FragilityBuffer * 2,
                Width + FragilityBuffer * 2);

        private bool _fragile;
        public bool Fragile
        {
            get => _fragile;
            set { _fragile = value; onPropertyChanged(); }
        }

        public bool IsFragile { get; internal set; }

        public class Sold
        {
            public DateTime Date { get; set; }
            public double Price { get; set; }
            
        }

        private List<Sold> _sold = new();
        public List<Sold> GetSold()
        {
            return _sold;
        }

        public void SetSold(List<Sold> value)
        { _sold = value; onPropertyChanged(); }
    }
}
