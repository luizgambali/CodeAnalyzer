using System.Text;
using CodeAnalyzer.App.Entities;

namespace CodeAnalyzer.App.Reports.GridView
{
    public class FileListGridView
    {
        public static string GenerateData(List<FileStatistics> listData)
        {
            var result = new StringBuilder();
            
            var data = listData.OrderBy(x => x.FileName).ToList();

            var maxSize = data.Max(x => x.FileName.Length);

            result.AppendLine("Code Files ".PadRight(maxSize+77, '-'));
            result.AppendLine("");

            result.AppendLine("File Name".PadRight(maxSize) + " | "
                    + "Lines of code |" 
                    + " Empty Lines  |" 
                    + " Comment Lines |" 
                    + " Total Lines  |"
                    + " Longest Line ");

            foreach(var item in data)
                result.AppendLine(item.FileName.PadRight(maxSize) + " | " 
                        + item.TotalLinesOfCode.ToString().PadLeft(13) + " | " 
                        + item.TotalLinesOfEmptySpace.ToString().PadLeft(12) + " | " 
                        + item.TotalLinesOfComments.ToString().PadLeft(13) + " | " 
                        + item.GetTotalLinesOfFile().ToString().PadLeft(12) + " | " 
                        + item.GetMaxLineLength().ToString().PadLeft(12));

            result.AppendLine("");

            return result.ToString();
        }
    }
}