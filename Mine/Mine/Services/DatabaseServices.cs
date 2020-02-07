using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Mine.Models;

namespace Mine
{
    class DatabaseServices
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseServices()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<ItemModel>> GetItemsAsync()
        {
            return Database.Table<ItemModel>().ToListAsync();
        }

        public Task<List<ItemModel>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<ItemModel>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<ItemModel> GetItemAsync(string id)
        {
            return Database.Table<ItemModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ItemModel item)
        {
            if (item.Id != null)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ItemModel item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
