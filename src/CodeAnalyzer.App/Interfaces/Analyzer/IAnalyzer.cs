using CodeAnalyzer.App.Entities;

namespace CodeAnalyzer.App.Interfaces.Analyzer
{
    public interface IAnalyzer
    {
        IEnumerable<FileStatistics> Analyze(string parentDirectory);
    }
}