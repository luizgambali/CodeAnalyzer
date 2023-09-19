namespace CodeAnalyzer.App.Entities.Report
{
    public class ProjectStatistics
    {
        public int TotalLinesOfCode { get; private set; }
        public int TotalLinesOfComments { get; private set; }
        public int TotalLinesOfEmptySpace { get; private set;}
        public int MaxLineLength { get; private set; }

        public ProjectStatistics(int totalLinesOfCode, int totalLinesOfComments, int totalLinesOfEmptySpace, int maxLineLength)
        {
            TotalLinesOfCode = totalLinesOfCode;
            TotalLinesOfComments = totalLinesOfComments;
            TotalLinesOfEmptySpace = totalLinesOfEmptySpace;
            MaxLineLength = maxLineLength;
        }

        public override bool Equals(object obj)
        {
            var projectStatistics = obj as ProjectStatistics;

            return projectStatistics != null &&
                   projectStatistics.TotalLinesOfCode == TotalLinesOfCode &&
                   projectStatistics.TotalLinesOfComments == TotalLinesOfComments &&
                   projectStatistics.TotalLinesOfEmptySpace == TotalLinesOfEmptySpace &&
                   projectStatistics.MaxLineLength == MaxLineLength;
        }

        public override int GetHashCode()
        {
           return (GetType().GetHashCode() * 113) + GetHashCode();
        }
    }
}