using System;
using System.Collections.Generic;

namespace GettingReal.Model
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int SKU { get; set; }
        public string StorageStatus { get; set; }
        public DateTime DateAcquired { get; set; }
        public double SalePrice { get; set; }
        public double ImportCost { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double FragilityBuffer { get; set; }
        public bool IsFragile { get; set; }

        public List<Sold> SoldItems { get; set; } = new List<Sold>();

        public class Sold
        {
            public DateTime Date { get; set; }
            public double Price { get; set; }
        }
    }
}
