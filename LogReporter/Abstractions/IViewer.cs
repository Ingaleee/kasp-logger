namespace ConsoleApp1.Abstractions;

public interface IViewer<in T>
{
    Task ViewAsync(T value);
}