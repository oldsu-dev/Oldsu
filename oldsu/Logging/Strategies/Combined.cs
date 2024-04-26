using System;
using System.Threading.Tasks;

namespace Oldsu.Logging.Strategies
{
    public class Combined : ILoggerWriterStrategy
    {
        private ILoggerWriterStrategy _strategy1;
        private ILoggerWriterStrategy _strategy2;
        
        public Combined(ILoggerWriterStrategy strategy1, ILoggerWriterStrategy strategy2)
        {
            _strategy1 = strategy1;
            _strategy2 = strategy2;
        }

        public Task LogInfo<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            return Task.WhenAll(
                _strategy1.LogInfo<T>(message, exception, dump, lineNumber, caller),
                _strategy2.LogInfo<T>(message, exception, dump, lineNumber, caller));
        }

        public Task LogCritical<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            return Task.WhenAll(
                _strategy1.LogCritical<T>(message, exception, dump, lineNumber, caller),
                _strategy2.LogCritical<T>(message, exception, dump, lineNumber, caller));
        }

        public Task LogFatal<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            return Task.WhenAll(
                _strategy1.LogFatal<T>(message, exception, dump, lineNumber, caller),
                _strategy2.LogFatal<T>(message, exception, dump, lineNumber, caller));
        }
    }
}