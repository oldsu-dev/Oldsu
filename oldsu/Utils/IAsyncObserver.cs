using System;
using System.Threading.Tasks;

namespace Oldsu.Utils
{
    public interface IAsyncObserver<in T>
    {
        Task OnNext(object? sender, T value);
        Task OnError(object? sender, Exception exception);
        Task OnCompleted(object? sender);
    }
}