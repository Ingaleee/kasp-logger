using System.Data;

namespace ConsoleApp1.Abstractions;

public interface ILogReporter
{
    LogReport FromTable(DataTable table);
}