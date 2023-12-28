namespace ConsoleApp1.Services;

public static class ArgumentsMonitor
{
    public static void ThrowIfInsufficiently(string[] args)
    {
        if (args.Length < 2)
        {
            throw new Exception("Usage run format: LogReporter <logs_path> <service_name>");
        }
    }
}