using System.Collections.Generic;

namespace Packaging.Storage
{
    /// <summary>
    /// Interface for at gemme/hente data.
    /// </summary>
    public interface IStorageService<T>
    {
        void Save(IEnumerable<T> items);
        List<T> Load();
    }
}
