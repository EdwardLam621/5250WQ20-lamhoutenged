using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mine.Services
{
    /// <summary>
    /// Interface for data intreactions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<ItemModel>
    {
        Task<bool> CreateAsync(ItemModel Data);
        Task<bool> UpdateAsync(ItemModel Data);
        Task<bool> DeleteAsync(string id);
        Task<ItemModel> ReadAsync(string id);
        Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false);
    }
}