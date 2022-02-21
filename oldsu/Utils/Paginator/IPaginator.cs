using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oldsu.Utils.Paginator
{
    public interface IPaginator<T>
    {
        public Task<List<T>> GetNewestPageAsync(int page);
    }
}