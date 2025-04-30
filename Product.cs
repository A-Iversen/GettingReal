using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReal
{
    internal class Product
    {
        // Properties - Attributes
        public string Name { get; set; }
        public string Category { get; set; }
        public int SKU { get; set; }
        public string StorageStatus { get; set; }
        public DateTime DateReceived { get; set; }
        public double SalePrice { get; set; }
        public double ImportCost { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool Fragile { get; set; }

        // Constructor
        public Product()
        {
            this.Name = Name;
            this.Category = Category;
            this.SKU = SKU;
            this.StorageStatus = StorageStatus;
            this.DateReceived = DateReceived;
            this.SalePrice = SalePrice;
            this.ImportCost = ImportCost;
            this.Length = Length;
            this.Width = Width;
            this.Height = Height;
            this.Fragile = Fragile;
        }


    }
}
