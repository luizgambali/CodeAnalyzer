using CodeAnalyzer.App.Entities.Report;

namespace CodeAnalyzer.App.Interfaces.Report
{
    public interface IFileStatisticsCalculate
    {
        FileStatisticsResume CalculateFileStatistics();
    }
}