using CodeAnalyzer.App.Reports.BarView;
using CodeAnalyzer.App.Reports.GridView;
using CodeAnalyzer.App.ValueObjects.Common;

namespace CodeAnalyzer.App.Report
{
    public class CompleteReport : Report 
    {
        
        public CompleteReport(StreamWriter streamWriter, List<FileStatistics> fileStatistics): base(streamWriter, fileStatistics)
        { 
        }

        public override void GenerateReport()
        {
            _streamWriter.Write(ProjectResumeGridView.GenerateData(_fileStatisticsResume.ProjectStatistics));
            _streamWriter.Flush();

            _streamWriter.Write(FileListGridView.GenerateData(_fileStatisticsResume.FileStatistics));
            _streamWriter.Flush();

            _streamWriter.Write(FilePercentageGridView.GenerateData(_fileStatisticsResume));
            _streamWriter.Flush();

            _streamWriter.Write(FilePercentageBarView.GenerateData(_fileStatisticsResume));
            _streamWriter.Flush();
        }
    }
}