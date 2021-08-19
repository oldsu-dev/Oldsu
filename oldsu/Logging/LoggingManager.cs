using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Oldsu.Logging.Strategies;

namespace Oldsu.Logging
{
    public class LoggingManager
    {
        public static readonly LoggingManager Empty = new LoggingManager(new NoLog());
        
        private readonly ILoggerWriterStrategy _logWriterTarget;

        public LoggingManager(ILoggerWriterStrategy logTarget)
        {
            _logWriterTarget = logTarget;
        }
        
        public async Task LogInfo<T>(
            string message, 
            Exception? exception = null,
            object? dump = null,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            if (caller == null) throw new ArgumentNullException(nameof(caller));
            
            await _logWriterTarget.LogInfo<T>(message, exception, dump, lineNumber, caller);
        }

        public async Task LogCritical<T>(
            string message, 
            Exception? exception = null,
            object? dump = null,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            if (caller == null) throw new ArgumentNullException(nameof(caller));

            await _logWriterTarget.LogCritical<T>(message, exception, dump, lineNumber, caller);
        }
        
        public async Task LogFatal<T>(
            string message, 
            Exception? exception = null,
            object? dump = null,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            if (caller == null) throw new ArgumentNullException(nameof(caller));

            await _logWriterTarget.LogFatal<T>(message, exception, dump, lineNumber, caller);
        }
    }
}