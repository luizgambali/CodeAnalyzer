using CodeAnalyzer.App.ValueObjects.Common;

namespace CodeAnalyzer.App.Interfaces.Analyzer
{
    public interface IAnalyzer
    {
        IEnumerable<FileStatistics> Analyze(string parentDirectory);
    }
}