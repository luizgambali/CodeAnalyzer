using CodeAnalyzer.App.ValueObjects.Common;
using CodeAnalyzer.App.ValueObjects.Report;

namespace CodeAnalyzer.App.Interfaces.Report
{
    public interface IFileStatisticsCalculate
    {
        FileStatisticsResume CalculateFileStatistics();
    }
}