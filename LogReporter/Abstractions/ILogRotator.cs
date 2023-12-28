namespace ConsoleApp1.Abstractions;

public interface ILogRotator
{
    Task<uint?> LastOrDefaultAsync(string service, string path);
}