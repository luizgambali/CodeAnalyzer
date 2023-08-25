using System.Text;
using CodeAnalyzer.App.ValueObjects.Report;

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

            var listData = data.FileStatistics.OrderByDescending(x => x.GetPercentageOfCode(data.ProjectStatistics.TotalLinesOfCode))
                                              .ThenByDescending(x => x.GetPercentageOfComments(data.ProjectStatistics.TotalLinesOfCode))
                                              .ThenByDescending(x => x.GetPercentageOfEmptySpace(data.ProjectStatistics.TotalLinesOfCode)).ToList();

            foreach(var item in listData)
                result.AppendLine(item.FileName.PadRight(maxSize) + " | " 
                        + item.GetPercentageOfCode(data.ProjectStatistics.TotalLinesOfCode).ToString().PadLeft(14) + " % | " 
                        + item.GetPercentageOfComments(data.ProjectStatistics.TotalLinesOfComments).ToString().PadLeft(14) + " % | " 
                        + item.GetPercentageOfEmptySpace(data.ProjectStatistics.TotalLinesOfEmptySpace).ToString().PadLeft(12) + " % |"  );

            result.AppendLine("");

            return result.ToString();
        }
    }
}