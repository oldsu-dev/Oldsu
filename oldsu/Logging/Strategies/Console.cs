using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Oldsu.Logging.Strategies
{
    public class Console : ILoggerWriterStrategy
    {
        public Task LogInfo<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            try
            {
                System.Console.WriteLine("[Info] {0}",
                    JsonConvert.SerializeObject(new
                    {
                        Message = message, Exception = exception, Dump = dump, LineNumber = lineNumber, Caller = caller
                    }));

            }
            catch
            {
                // ignored
            }
            return Task.CompletedTask;
        }

        public Task LogCritical<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            try
            {
                System.Console.WriteLine("[Critical] {0}",
                    JsonConvert.SerializeObject(new
                    {
                        Message = message, Exception = exception, Dump = dump, LineNumber = lineNumber, Caller = caller
                    }));
            }
            catch
            {
                //ignored
            }

            return Task.CompletedTask;
        }

        public Task LogFatal<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            try
            {
                System.Console.WriteLine("[Fatal] {0}",
                    JsonConvert.SerializeObject(new
                    {
                        Message = message, Exception = exception, Dump = dump, LineNumber = lineNumber, Caller = caller
                    }));
            }
            catch
            {
                //ignored
            }

            return Task.CompletedTask;
        }
    }
}