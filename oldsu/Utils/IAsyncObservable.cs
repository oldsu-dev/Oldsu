using System;
using System.Threading.Tasks;

namespace Oldsu.Utils
{
    public interface IAsyncObservable<out T>
    {
        Task<IAsyncDisposable> SubscribeAsync(IAsyncObserver<T> observer);
    }
}