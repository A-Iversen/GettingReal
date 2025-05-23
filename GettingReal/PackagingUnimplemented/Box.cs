using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Packaging
{
    /// <summary>
    /// Repræsenterer en emballageboks.
    /// </summary>
    public class Box
    {
        // Unikt navn (ingen dubletter)
        public string Name { get; set; }

        // Ydre mål i cm
        public double OuterLength { get; set; }
        public double OuterWidth { get; set; }
        public double OuterHeight { get; set; }

        // Vægt i gram
        public double Weight { get; set; }

        // Beregnet volumen (ydre mål)
        [JsonIgnore]
        public double Volume => OuterLength * OuterWidth * OuterHeight;

        // Beregnede indre mål (kan overskrives manuelt af bruger)
        public double InnerLength { get; set; }
        public double InnerWidth { get; set; }
        public double InnerHeight { get; set; }

        /// <summary>
        /// Standardberegning af indre mål:
        /// - Længste side: -2 cm
        /// - De to andre sider: -1 cm
        /// </summary>
        public void CalculateInnerDimensions()
        {
            var sides = new List<double> { OuterLength, OuterWidth, OuterHeight };
            sides.Sort(); // sides[2] er den længste
            InnerLength = sides[2] - 2;
            InnerWidth = sides[1] - 1;
            InnerHeight = sides[0] - 1;

            // Valider for at undgå negative indre dimensioner
            if (InnerLength <= 0 || InnerWidth <= 0 || InnerHeight <= 0)
                throw new InvalidOperationException("Inner dimensions must be positive. Check outer dimensions.");
        }
    }
}
