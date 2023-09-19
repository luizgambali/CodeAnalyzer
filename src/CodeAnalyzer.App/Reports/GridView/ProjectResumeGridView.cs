using System.Text;
using CodeAnalyzer.App.Entities.Report;

namespace CodeAnalyzer.App.Reports.GridView
{
    public class ProjectResumeGridView
    {
        public static string GenerateData(ProjectStatistics project)
        {
            var result = new StringBuilder();
            
            result.AppendLine("Project Resume ".PadRight(62, '-'));
            result.AppendLine("");

            result.AppendLine("Lines of code |" 
                            + " Empty Lines  |" 
                            + " Comment Lines |" 
                            + " Max Line Length ");

            result.AppendLine(project.TotalLinesOfCode.ToString().PadLeft(13) + " | " 
                            + project.TotalLinesOfEmptySpace.ToString().PadLeft(12) + " | " 
                            + project.TotalLinesOfComments.ToString().PadLeft(13) + " | "
                            + project.MaxLineLength.ToString().PadLeft(15));
            
            result.AppendLine("");

            return result.ToString();
        }
    }
}