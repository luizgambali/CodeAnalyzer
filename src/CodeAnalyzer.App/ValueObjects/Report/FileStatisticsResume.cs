using CodeAnalyzer.App.ValueObjects.Common;

namespace CodeAnalyzer.App.ValueObjects.Report
{   
    public class FileStatisticsResume
    {
        public List<FileStatistics> FileStatistics { get; private set; }
        public ProjectStatistics ProjectStatistics { get; private set; }
        
        public FileStatisticsResume(List<FileStatistics> fileStatistics, ProjectStatistics projectStatistics)
        {
            FileStatistics = fileStatistics;
            ProjectStatistics = projectStatistics;
        }
    }
}