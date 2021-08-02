using System;
using System.Threading.Tasks;

namespace Oldsu.Utils
{
    public interface IAsyncObserver<in T>
    {
        Task OnNextAsync(T value);
        Task OnErrorAsync(Exception exception);
        Task OnCompletedAsync();
    }
}