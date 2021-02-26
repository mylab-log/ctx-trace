using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace FunctionTest
{
    class StringBuilderLogger : ILogger
    {
        private readonly StringBuilder _sb;

        public StringBuilderLogger(StringBuilder sb)
        {
            _sb = sb;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _sb.AppendLine(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }

    class StringBuilderLoggerProvider : ILoggerProvider
    {
        private readonly StringBuilder _sb;

        public StringBuilderLoggerProvider(StringBuilder sb)
        {
            _sb = sb;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new StringBuilderLogger(_sb);
        }
    }
}
