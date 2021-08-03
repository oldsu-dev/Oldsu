using System;
using System.Threading.Tasks;

namespace Oldsu.Utils
{
    public interface IAsyncObservable<T>
    {
        Task<IAsyncDisposable> Subscribe(IAsyncObserver<T> observer);
        Task Notify(T data);
        Task Complete();
        Task Error(Exception exception);
    }
}