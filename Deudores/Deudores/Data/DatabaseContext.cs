using Deudores.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Deudores.Data
{
    public class DatabaseContext
    {
        public SQLiteAsyncConnection Connection { get; set; }

        public DatabaseContext(string path)
        {
            Connection = new SQLiteAsyncConnection(path);
            Connection.CreateTableAsync<Deudor>().Wait();
        }

        public async Task<int> InsertItemAsync(Deudor deudor)
        {
            return await Connection.InsertAsync(deudor);
        }

        public async Task<List<Deudor>> GetItemAsync()
        {
            return await Connection.Table<Deudor>().ToListAsync();
        }
        public async Task<int> DeleteItemAsync(Deudor deudor)
        {
            return await Connection.DeleteAsync(deudor);
        }

        public async Task<int> UpdateItemAsync(Deudor deudor)
        {
            return await Connection.UpdateAsync(deudor);
        }

        public async Task<Deudor> ObtenerDeudorAsync(int id)
        {
            return await Connection.Table<Deudor>().FirstOrDefaultAsync(c=>c.Id==id);
        }
    }
}
