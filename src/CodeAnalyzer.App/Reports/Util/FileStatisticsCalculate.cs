using CodeAnalyzer.App.Interfaces.Report;
using CodeAnalyzer.App.Entities;
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
            var result = new FileStatisticsResume(listOfData);
            return result;
        }
    }
}