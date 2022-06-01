using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Oldsu.Utils.Paginator
{
    public class MySqlEntityFrameworkPaginator<T> : IPaginator<T> where T : class
    {
        private DbSet<T> _dbSet;
        private int _rowsPerPage;

        public MySqlEntityFrameworkPaginator(DbSet<T> dbSet, int rowsPerPage)
        {
            _dbSet = dbSet;
            _rowsPerPage = rowsPerPage;
        }

        public async Task<List<T>> GetNewestPageAsync(int page)
        {
            return await _dbSet
                .Skip(_rowsPerPage * page)
                .Take(_rowsPerPage)
                .ToListAsync();
        }
    }
}