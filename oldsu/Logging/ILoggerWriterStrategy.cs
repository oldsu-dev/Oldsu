using System;
using System.Threading.Tasks;

namespace Oldsu.Logging.Strategies
{
    public interface ILoggerWriterStrategy
    {
        public Task LogInfo<T>(  
            string message, 
            Exception? exception,
            object? dump,
            int lineNumber,
            string caller);
        
        public Task LogCritical<T>(
            string message, 
            Exception? exception,
            object? dump,
            int lineNumber,
            string caller);
        
        public Task LogFatal<T>(
            string message,
            Exception? exception,
            object? dump,
            int lineNumber,
            string caller);
    }
}