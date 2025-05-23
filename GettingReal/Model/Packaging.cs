using System;
using GettingReal.Model;

namespace GettingReal.Model
{
    public class Packaging : Product
    {
        // Properties Length, Width, Height, and Fragile bliver inheritet fra Product så skal ikke deklares her

        public void CalculatePackaging(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
            
            onPropertyChanged(nameof(Length));
            onPropertyChanged(nameof(Width));
            onPropertyChanged(nameof(Height));
        }
    }
}