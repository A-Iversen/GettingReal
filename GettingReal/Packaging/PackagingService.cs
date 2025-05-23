// Packaging/PackagingService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using GettingReal.Model;

namespace Packaging
{
    /// <summary>
    /// Service to find the optimal box for one or two products.
    /// </summary>
    public static class PackagingService
    {
        /// <summary>
        /// Finds the smallest box that fits a single product (including fragility buffer).
        /// Returns null if no suitable box is found.
        /// </summary>
        public static Box? GetOptimalBox(Product product, IEnumerable<Box> boxes)
        {
            var dims = product.DimensionsWithBuffer;
            var candidate = SortAscending(dims.L, dims.H, dims.W);
            return FindBestBoxForComposite(new[] { candidate }, boxes);
        }

        /// <summary>
        /// Finds the smallest box that fits two products packed together (including fragility buffers).
        /// Returns null if no suitable box is found.
        /// </summary>
        public static Box? GetOptimalBox(Product product1, Product product2, IEnumerable<Box> boxes)
        {
            var perms1 = GetPermutations(
                product1.DimensionsWithBuffer.L,
                product1.DimensionsWithBuffer.H,
                product1.DimensionsWithBuffer.W);

            var perms2 = GetPermutations(
                product2.DimensionsWithBuffer.L,
                product2.DimensionsWithBuffer.H,
                product2.DimensionsWithBuffer.W);

            var candidates = new List<(double l, double h, double w)>();

            foreach (var d1 in perms1)
                foreach (var d2 in perms2)
                {
                    // side-by-side along length (X)
                    candidates.Add((d1.l + d2.l,
                                    Math.Max(d1.h, d2.h),
                                    Math.Max(d1.w, d2.w)));
                    // side-by-side along height (Y)
                    candidates.Add((Math.Max(d1.l, d2.l),
                                    d1.h + d2.h,
                                    Math.Max(d1.w, d2.w)));
                    // side-by-side along width (Z)
                    candidates.Add((Math.Max(d1.l, d2.l),
                                    Math.Max(d1.h, d2.h),
                                    d1.w + d2.w));
                }

            return FindBestBoxForComposite(candidates, boxes);
        }

        // Finds the lowest-volume box that fits any candidate dimension set.
        private static Box? FindBestBoxForComposite(
            IEnumerable<(double l, double h, double w)> candidates,
            IEnumerable<Box> boxes)
        {
            var boxInfos = boxes
                .OrderBy(b => b.Volume)
                .Select(b => new
                {
                    Box = b,
                    Volume = b.Volume,
                    Sorted = SortAscending(b.InnerLength, b.InnerHeight, b.InnerWidth)
                })
                .ToList();

            Box? bestBox = null;
            double bestVolume = double.MaxValue;

            foreach (var cand in candidates)
            {
                var cs = SortAscending(cand.l, cand.h, cand.w);

                foreach (var info in boxInfos)
                {
                    if (info.Sorted.l >= cs.l &&
                        info.Sorted.h >= cs.h &&
                        info.Sorted.w >= cs.w)
                    {
                        if (info.Volume < bestVolume)
                        {
                            bestVolume = info.Volume;
                            bestBox = info.Box;
                        }
                        break; // stop scanning larger boxes for this candidate
                    }
                }
            }

            return bestBox;
        }

        // Sort three values ascending; return a named tuple.
        private static (double l, double h, double w) SortAscending(
            double x, double y, double z)
        {
            var arr = new[] { x, y, z };
            Array.Sort(arr);
            return (arr[0], arr[1], arr[2]);
        }

        // Generate all 6 axis-permutations of (x,y,z).
        private static IEnumerable<(double l, double h, double w)> GetPermutations(
            double x, double y, double z)
        {
            yield return (x, y, z);
            yield return (x, z, y);
            yield return (y, x, z);
            yield return (y, z, x);
            yield return (z, x, y);
            yield return (z, y, x);
        }
    }
}