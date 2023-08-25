using CodeAnalyzer.App.Interfaces.Report;
using CodeAnalyzer.App.Reports.Util;
using CodeAnalyzer.App.ValueObjects.Common;
using CodeAnalyzer.App.ValueObjects.Report;

namespace CodeAnalyzer.App.Report
{
    public abstract class Report
    {
        protected List<FileStatistics> _fileStatistics;
        protected FileStatisticsResume _fileStatisticsResume;
        protected StreamWriter _streamWriter;
        protected IFileStatisticsCalculate _calculate;

        public Report(StreamWriter streamWriter,  List<FileStatistics> fileStatistics)
        {
            _streamWriter = streamWriter;
            _fileStatistics = fileStatistics;

            if (_streamWriter == null)
                throw new ArgumentException("The stream writer is null.");

            if (_fileStatistics == null && _fileStatistics.Count == 0)
                throw new ArgumentException("The list of file statistics is empty.");

            GenerateDataToReport();
        }
        
        protected void GenerateDataToReport()
        {                        
            _calculate = new FileStatisticsCalculate(_fileStatistics);
            _fileStatisticsResume = _calculate.CalculateFileStatistics();
        }

        public abstract void GenerateReport();
    }
}