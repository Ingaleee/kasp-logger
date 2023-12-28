using ConsoleApp1.Abstractions;

namespace ConsoleApp1.Services;

public class ConsoleLogViewer : IViewer<LogReport>
{
    public async Task ViewAsync(LogReport value)
    {
        Console.WriteLine($"Early log write time: {value.EarlyLineTime:dd.MM.yyyy HH:mm:ss.fff}");
        Console.WriteLine($"Latest log write time: {value.LatestLineTime:dd.MM.yyyy HH:mm:ss.fff}");

        foreach (var severity in value.Severities)
        {
            Console.WriteLine($"Severity [{severity.Name}] count is {severity.Count} ({severity.Percent}%)");
        }
        
        Console.WriteLine($"Log rotations: {value.Rotations}");
    }
}