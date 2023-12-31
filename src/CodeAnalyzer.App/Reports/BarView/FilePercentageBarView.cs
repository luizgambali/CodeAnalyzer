using System.Text;
using CodeAnalyzer.App.Entities.Report;

namespace CodeAnalyzer.App.Reports.BarView
{
    public class FilePercentageBarView
    {
        public static string GenerateData(FileStatisticsResume data)
        {
            var result = new StringBuilder();
            
            var maxSize = data.FileStatistics.Max(x => x.FileName.Length);

            result.AppendLine("Code Files Statistics ".PadRight(maxSize + 100, '-'));
            result.AppendLine("");

            var projectStatistics = data.GetProjectStatistics();

            var listData = data.FileStatistics.OrderByDescending(x => x.GetPercentageOfCode(projectStatistics.TotalLinesOfCode))
                                              .ThenByDescending(x => x.GetPercentageOfComments(projectStatistics.TotalLinesOfCode))
                                              .ThenByDescending(x => x.GetPercentageOfEmptySpace(projectStatistics.TotalLinesOfCode)).ToList();

            result.AppendLine("File Name".PadRight(maxSize)  + " % Lines of Code");

            foreach(var item in listData)
                result.AppendLine(item.FileName.PadRight(maxSize) + " " + GenerateBar(item.GetPercentageOfCode(projectStatistics.TotalLinesOfCode)));

            result.AppendLine("");

            return result.ToString();
        }

        private static string GenerateBar(double percentage)
        {
            if (percentage <= 0)
                return string.Empty;

            if (percentage > 0 && percentage < 1)
                return "| " + percentage.ToString("0#.00") + "% ";
            else
            {
                var result = "";

                for(int i = 0; i < percentage; i++)
                    result += "#"; 

                return result + " " + percentage.ToString("#.00") + "%";
            }
        }
    }
}