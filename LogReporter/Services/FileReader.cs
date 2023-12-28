using ConsoleApp1.Abstractions;

namespace ConsoleApp1.Services;

public class LogsFileReader : IFileReader
{
    public async Task<IEnumerable<string>> AllFilesContentAsync(string path, string? mask = null)
    {
        var allLines = new List<string>();
        var files = Directory.GetFiles(path, mask ?? "*", SearchOption.TopDirectoryOnly);
        foreach (var file in files)
        {
            var content = await File.ReadAllLinesAsync(file);
            allLines.AddRange(content);
        }

        return allLines;
    }
    
}