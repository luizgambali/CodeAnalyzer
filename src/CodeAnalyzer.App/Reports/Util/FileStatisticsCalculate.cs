using CodeAnalyzer.App.Interfaces.Report;
using CodeAnalyzer.App.ValueObjects.Common;
using CodeAnalyzer.App.ValueObjects.Report;

namespace CodeAnalyzer.App.Reports.Util
{
    public class FileStatisticsCalculate : IFileStatisticsCalculate
    {
        private List<FileStatistics> listOfData = new List<FileStatistics>();

        public FileStatisticsCalculate(List<FileStatistics> listOfData)
        {
            this.listOfData = listOfData;

            if (listOfData == null || listOfData.Count == 0)
                throw new ArgumentException("The list statistics is null.");
        }

        public FileStatisticsResume CalculateFileStatistics()
        {
            var projectStatistics = CalculateProjectStatistic();
            var result = new FileStatisticsResume(listOfData, projectStatistics);
            return result;
        }

        private ProjectStatistics CalculateProjectStatistic()
        {            
            var totalLines = listOfData.Sum(x => x.GetTotalLinesOfFile());
            var totalLinesOfEmptySpace = listOfData.Sum(x => x.TotalLinesOfEmptySpace);
            var totalLinesOfComments = listOfData.Sum(x => x.TotalLinesOfComments);
            var totalLinesOfCode = listOfData.Sum(x => x.TotalLinesOfCode);
            var maxLineLength = listOfData.Max(x => x.MaxLineLength);

            return new ProjectStatistics(totalLinesOfCode, totalLinesOfComments, totalLinesOfEmptySpace, maxLineLength);
        }
    }
}