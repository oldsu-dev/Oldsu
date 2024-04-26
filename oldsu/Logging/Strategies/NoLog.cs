using System;
using System.Threading.Tasks;

namespace Oldsu.Logging.Strategies
{
    public class NoLog : ILoggerWriterStrategy
    {
        public Task LogInfo<T>(string message, Exception? exception, object? dump, int lineNumber, string caller) =>
            Task.CompletedTask;

        public Task LogCritical<T>(string message, Exception? exception, object? dump, int lineNumber, string caller) =>
            Task.CompletedTask;

        public Task LogFatal<T>(string message, Exception? exception, object? dump, int lineNumber, string caller) =>
            Task.CompletedTask;
    }
}