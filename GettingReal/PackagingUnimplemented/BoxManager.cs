using System;
using System.Collections.Generic;
using System.Linq;
using Packaging.Storage;

namespace Packaging
{
    /// <summary>
    /// Ansvarlig for CRUD-operationer på Bokse + sortering og JSON-persistens.
    /// </summary>
    public class BoxManager
    {
        private readonly IStorageService<Box> _storage;
        private readonly List<Box> _boxes = new();

        public BoxManager(IStorageService<Box> storage)
        {
            _storage = storage;
            Load();
        }

        // Liste af bokse, sorteret efter volumen
        public IEnumerable<Box> Boxes => _boxes.OrderBy(b => b.Volume);

        public void AddBox(Box box)
        {
            ValidateBox(box);
            if (_boxes.Any(b => b.Name.Equals(box.Name, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"A box with the name '{box.Name}' already exists.");

            box.CalculateInnerDimensions();
            _boxes.Add(box);
            Save();
        }

        public void RemoveBox(string name)
        {
            var box = _boxes.SingleOrDefault(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (box == null)
                throw new KeyNotFoundException($"No box with name '{name}' to remove.");

            _boxes.Remove(box);
            Save();
        }

        public void EditBox(Box updated)
        {
            ValidateBox(updated);
            var idx = _boxes.FindIndex(b => b.Name.Equals(updated.Name, StringComparison.OrdinalIgnoreCase));
            if (idx == -1)
                throw new KeyNotFoundException($"No box with name '{updated.Name}' to edit.");

            updated.CalculateInnerDimensions();
            _boxes[idx] = updated;
            Save();
        }

        private void ValidateBox(Box box)
        {
            if (string.IsNullOrWhiteSpace(box.Name))
                throw new ArgumentException("Box name cannot be empty.");
            foreach (var dim in new[] { box.OuterLength, box.OuterWidth, box.OuterHeight })
                if (dim <= 0)
                    throw new ArgumentException("Outer dimensions must be positive numbers.");
            if (box.Weight < 0)
                throw new ArgumentException("Weight cannot be negative.");
        }

        public void Save() => _storage.Save(_boxes);

        public void Load()
        {
            var loaded = _storage.Load();
            _boxes.Clear();
            _boxes.AddRange(loaded);
        }
    }
}
