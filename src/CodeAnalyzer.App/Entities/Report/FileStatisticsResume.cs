using CodeAnalyzer.App.Entities;

namespace CodeAnalyzer.App.Entities.Report
{   
    public class FileStatisticsResume
    {
        public List<FileStatistics> FileStatistics { get; private set; }
        private ProjectStatistics projectStatistics;
        
        public FileStatisticsResume(List<FileStatistics> fileStatistics)
        {
            FileStatistics = fileStatistics;

            if (fileStatistics == null || fileStatistics.Count() == 0)
                throw new ArgumentException("There is no data to generate statistics");

            projectStatistics = GetProjectStatistics();
        }

        public ProjectStatistics GetProjectStatistics()
        {            
            var totalLines = FileStatistics.Sum(x => x.TotalLinesOfCode);
            var totalLinesOfEmptySpace = FileStatistics.Sum(x => x.TotalLinesOfEmptySpace);
            var totalLinesOfComments = FileStatistics.Sum(x => x.TotalLinesOfComments);
            var totalLinesOfCode = FileStatistics.Sum(x => x.TotalLinesOfCode);
            var maxLineLength = FileStatistics.Max(x => x.MaxLineLength);

            return new ProjectStatistics(totalLines, totalLinesOfComments, totalLinesOfEmptySpace, maxLineLength);
        }
    }
}