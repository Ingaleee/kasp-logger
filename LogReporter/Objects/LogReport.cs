using ConsoleApp1.Objects;

namespace ConsoleApp1;

public class LogReport
{
    public DateTime EarlyLineTime { get; set; }
    public DateTime LatestLineTime { get; set; }
    public IEnumerable<SeverityReport> Severities { get; set; }
    public long Rotations { get; set; }
}