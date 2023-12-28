namespace ConsoleApp1.Abstractions;

public interface IFileReader
{
    Task<IEnumerable<string>> AllFilesContentAsync(string path, string mask);
}