using System.Collections.Generic;
using System.Linq;
using GettingReal.Model; // Tilføjet af PU mht Packaging

namespace Packaging
{
    /// <summary>
    /// Service to find the optimal box for a given product.
    /// </summary>
    public static class PackagingService
    {
        /// <summary>
        /// Finds the smallest box that fits the product including fragility buffer.
        /// </summary>
        /// <param name="product">The product with fragility buffer.</param>
        /// <param name="boxes">The collection of available boxes.</param>
        /// <returns>The optimal box, or null if none fits.</returns>
        public static Box? GetOptimalBox(Product product, IEnumerable<Box> boxes)
        {
            var (l, h, w) = product.DimensionsWithBuffer;
            return boxes
                .Where(b =>
                    b.InnerLength >= l &&
                    b.InnerHeight >= h &&
                    b.InnerWidth >= w)
                .OrderBy(b => b.Volume)
                .FirstOrDefault();
        }
    }
}
