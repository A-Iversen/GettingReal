using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization; // Tilføjet af PU mht Packaging

namespace GettingReal.Model
{
    public class Product
    {
        // Properties
        public string Name { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public bool IsFragile { get; set; }

        // Tilføjet af PU mht Packaging
        /// <summary>
        /// Fragility buffer in cm (e.g. 0.5 or 1.0)
        /// </summary>
        public double FragilityBuffer { get; set; }

        // Tilføjet af PU mht Packaging
        /// <summary>
        /// Total dimensions including buffer on each side
        /// </summary>
        [JsonIgnore]
        public (double L, double H, double W) DimensionsWithBuffer
            => (Length + FragilityBuffer * 2,
                Height + FragilityBuffer * 2,
                Width + FragilityBuffer * 2);

        // Constructor - Automatic
        public Product()
        {
        }

        // Override ToString for file storage
        public override string ToString()
        {
            return $"{Name}|{Length}|{Height}|{Width}|{IsFragile}";
        }

        // Parse from string (for loading from file)
        public static Product FromString(string line)
        {
            var parts = line.Split('|');
            return new Product
            {
                Name = parts[0],
                Length = double.Parse(parts[1]),
                Height = double.Parse(parts[2]),
                Width = double.Parse(parts[3]),
                IsFragile = bool.Parse(parts[4])
            };
        }
    }
}
