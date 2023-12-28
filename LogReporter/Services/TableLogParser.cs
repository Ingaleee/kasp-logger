using System.Data;
using System.Globalization;
using ConsoleApp1.Abstractions;
using ConsoleApp1.Objects;

namespace ConsoleApp1.Services;

public class DataRowLogParser : ILogParser<LogLine>
{
    public async Task<IEnumerable<LogLine>> ParseAsync(IEnumerable<string> lines)
    {
        var result = new List<LogLine>();
        foreach (var line in lines)
        {
            var lineParts = line.Split(']').Select(x => x.Replace("[", "").Trim()).ToArray();

            var correctedDateFormat = DateTime.TryParseExact(lineParts[0], 
                "dd.MM.yyyy HH:mm:ss.fff",
                CultureInfo.InvariantCulture,  
                DateTimeStyles.None, 
                out var date);

            if (!correctedDateFormat)
            {
                continue;;
            }

            var severity = lineParts[1];
            var category = lineParts[2];
            
            result.Add(new LogLine
            {
                Date = date,
                Category = category,
                Severity = severity
            });
            
        }

        return result;
    }
}