using CodeAnalyzer.App.Entities;

namespace CodeAnalyzer.App.Interfaces.Parser
{
    public interface IFileParser
    {
        FileStatistics Parse(string filePath);
    }
}