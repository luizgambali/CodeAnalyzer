using CodeAnalyzer.App.ValueObjects.Common;

namespace CodeAnalyzer.App.Interfaces.Parser
{
    public interface IFileParser
    {
        FileStatistics Parse(string filePath);
    }
}