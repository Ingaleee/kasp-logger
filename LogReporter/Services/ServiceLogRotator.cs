using ConsoleApp1.Abstractions;

namespace ConsoleApp1.Services;

public class ServiceLogRotator : ILogRotator
{
    public async Task<uint?> LastOrDefaultAsync(string service, string path)
    {
        var logs = Directory.GetFiles(path, $"{service}.*", SearchOption.TopDirectoryOnly);
        var latestLog = logs.MaxBy(l =>
        {
            var stringifyRotation = Path.GetFileNameWithoutExtension(l)
                .Replace($"{service}.", string.Empty);
            return uint.TryParse(stringifyRotation, out var rotation) ? rotation : 0;
        });
        var latestRotationString = Path.GetFileNameWithoutExtension(latestLog)
            .Replace($"{service}.", string.Empty);
        
        return uint.TryParse(latestRotationString, out var latestRotationNumber) ? 
            latestRotationNumber : null;
    }
}