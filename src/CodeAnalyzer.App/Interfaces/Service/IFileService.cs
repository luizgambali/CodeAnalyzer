namespace CodeAnalyzer.App.Interfaces.Service
{
    public interface IFileService
    {
        List<string> GetFilesFromParentDirectory(string parentDirectory);
    }
}