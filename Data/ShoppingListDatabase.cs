using SQLite;
using Trif_Andrei_Lab7.Models;

namespace Trif_Andrei_Lab7.Data
{
    public class ShoppingListDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
        }

        public Task<List<ShopList>> GetShopListsAsync()
        {
            return _database.Table<ShopList>().ToListAsync();
        }

        public Task<ShopList> GetShopListAsync(int id)
        {
            return _database.Table<ShopList>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveShopListAsync(ShopList slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }

        public Task<int> DeleteShopListAsync(ShopList slist)
        {
            return _database.DeleteAsync(slist);
        }

        public Task<int> SaveProductAsync(Product product) 
        {
            if (product.ID != 0) 
            {
                return _database.UpdateAsync(product); 
            } 
            else 
            { 
                return _database.InsertAsync(product); 
            } 
        }

        public Task<int> DeleteProductAsync(Product product) 
        { 
            return _database.DeleteAsync(product); 
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveListProductAsync(ListProduct listp) 
        { 
            if (listp.ID != 0) 
            { 
                return _database.UpdateAsync(listp); 
            } 
            else 
            { 
                return _database.InsertAsync(listp); 
            } 
        }

        public async Task<int> DeleteListProductAsync(ListProduct listp)
        {
            var listProduct = await _database.Table<ListProduct>()
                .Where(i => i.ProductID == listp.ProductID && i.ShopListID == listp.ShopListID)
                .FirstOrDefaultAsync();        

            return await _database.DeleteAsync(listProduct);
        }

        public Task<List<Product>> GetListProductsAsync(int shoplistid)
        {
            return _database.QueryAsync<Product>(
                "select P.ID, P.Description from Product P" + 
                " inner join ListProduct LP" + 
                " on P.ID = LP.ProductID where LP.ShopListID = ?", shoplistid);
        }
    }
}
