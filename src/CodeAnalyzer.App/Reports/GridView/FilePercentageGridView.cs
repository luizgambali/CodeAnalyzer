using System.Text;
using CodeAnalyzer.App.Entities.Report;

namespace CodeAnalyzer.App.Reports.GridView
{
    public class FilePercentageGridView
    {
        public static string GenerateData(FileStatisticsResume data)
        {
            var result = new StringBuilder();
            
            var maxSize = data.FileStatistics.Max(x => x.FileName.Length);

            result.AppendLine("Code Files Statistics ".PadRight(maxSize+57, '-'));
            result.AppendLine("");

            result.AppendLine("File Name".PadRight(maxSize) + " | "
                    + " % Lines of code | " 
                    + " % Comment Lines | " 
                    + " % Empty Lines |");

            var projectStatistics = data.GetProjectStatistics();

            var listData = data.FileStatistics.OrderByDescending(x => x.GetPercentageOfCode(projectStatistics.TotalLinesOfCode))
                                              .ThenByDescending(x => x.GetPercentageOfComments(projectStatistics.TotalLinesOfCode))
                                              .ThenByDescending(x => x.GetPercentageOfEmptySpace(projectStatistics.TotalLinesOfCode)).ToList();

            foreach(var item in listData)
                result.AppendLine(item.FileName.PadRight(maxSize) + " | " 
                        + item.GetPercentageOfCode(projectStatistics.TotalLinesOfCode).ToString().PadLeft(14) + " % | " 
                        + item.GetPercentageOfComments(projectStatistics.TotalLinesOfComments).ToString().PadLeft(14) + " % | " 
                        + item.GetPercentageOfEmptySpace(projectStatistics.TotalLinesOfEmptySpace).ToString().PadLeft(12) + " % |"  );

            result.AppendLine("");

            return result.ToString();
        }
    }
}