using ConsoleApp1.Objects;

namespace ConsoleApp1.Abstractions;

public interface ILogParser<out TResult>
{
    Task<IEnumerable<LogLine>> ParseAsync(IEnumerable<string> lines);
}