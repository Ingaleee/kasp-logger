using System.Data;
using ConsoleApp1.Abstractions;
using ConsoleApp1.Objects;

namespace ConsoleApp1.Services;

public class ServiceLogReporter : ILogReporter
{
    public LogReport FromTable(DataTable table)
    {
        var enumerableTable = table.AsEnumerable();

        var severities  = enumerableTable.Select(x => x["Severity"] as string).ToList();
        var uniqueSeverities = severities.Distinct();

        var severityAnalysis = new List<SeverityReport>();
        foreach (var name in uniqueSeverities)
        {
            var count = severities.Count(x => x == name);
            var percent = (decimal)count / severities.Count * 100;
            
            severityAnalysis.Add(new SeverityReport
            {
                Name = name,
                Count = (uint)count,
                Percent = (uint)percent
            });
        }
        
        var earlyLineTime = (DateTime)enumerableTable.Min(x => x["Date"]);
        var latestLineTime = (DateTime)enumerableTable.Max(x => x["Date"]);

        return new LogReport
        {
            EarlyLineTime = earlyLineTime,
            LatestLineTime = latestLineTime,
            Severities = severityAnalysis
        };
    }
}