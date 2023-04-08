using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Test
{
    internal class MockLogger<T> : ILogger<T>
    {
        public List<string> LogMessages { get; } = new List<string>();

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LogMessages.Add(formatter(state, exception));
        }
    }
}
