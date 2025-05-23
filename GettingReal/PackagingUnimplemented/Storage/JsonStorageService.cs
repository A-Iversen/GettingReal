using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Packaging.Storage
{
    /// <summary>
    /// JSON-implementering af IStorageService.
    /// </summary>
    public class JsonStorageService<T> : IStorageService<T>
    {
        private readonly string _filePath;

        public JsonStorageService(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(IEnumerable<T> items)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(items, options));
        }

        public List<T> Load()
        {
            if (!File.Exists(_filePath))
                return new List<T>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json)
                   ?? new List<T>();
        }
    }
}
