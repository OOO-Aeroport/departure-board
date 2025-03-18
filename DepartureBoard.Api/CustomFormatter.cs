using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace DepartureBoard.Api;

public class CustomFormatter(IOptionsMonitor<ConsoleFormatterOptions> options)
    : ConsoleFormatter("custom")
{
    private struct AnsiColors
    {
        public const string Reset = "\e[0m";
        public const string Green = "\e[32m";
        public const string Yellow = "\e[33m";
        public const string Red = "\e[31m";
        public const string Magenta = "\e[35m";
        public const string BlackBackground = "\e[40m";
    }
    
    public override void Write<TState>(
        in LogEntry<TState> entry,
        IExternalScopeProvider? provider,
        TextWriter writer)
    {
        var color = GetLogLevelColor(entry.LogLevel);
        var message = entry.Formatter(entry.State, entry.Exception);
        writer.WriteLine($"({DateTime.Now}) {color}{entry.LogLevel}{AnsiColors.Reset}: {message}");
    }

    private static string GetLogLevelColor(LogLevel level)
    {
        return level switch
        {
            LogLevel.Information => AnsiColors.Green + AnsiColors.BlackBackground,
            LogLevel.Warning => AnsiColors.Yellow + AnsiColors.BlackBackground,
            LogLevel.Error => AnsiColors.Red + AnsiColors.BlackBackground,
            LogLevel.Critical => AnsiColors.Magenta + AnsiColors.BlackBackground,
            _ => AnsiColors.Reset
        };
    }
}