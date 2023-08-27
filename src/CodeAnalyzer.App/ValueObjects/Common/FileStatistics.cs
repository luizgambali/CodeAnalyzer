namespace CodeAnalyzer.App.ValueObjects.Common
{
    public class FileStatistics
    {
        public string FileName { get; private set; }
        public int TotalLinesOfCode { get; private set; }
        public int TotalLinesOfComments { get; private set; }
        public int TotalLinesOfEmptySpace { get; private set;}
        public int MaxLineLength { get; private set; }

        public FileStatistics(string fileName)
        {
            FileName = fileName;
            ResetCounters();
        }

        public FileStatistics(string fileName, int totalLinesOfCode, int totalLinesOfComments, int totalLinesOfEmptySpace, int maxLineLength)
        {
            FileName = fileName;
            TotalLinesOfCode = totalLinesOfCode;
            TotalLinesOfComments = totalLinesOfComments;
            TotalLinesOfEmptySpace = totalLinesOfEmptySpace;
            MaxLineLength = maxLineLength;
        }

        public void ResetCounters()
        {
            TotalLinesOfCode = 0;
            TotalLinesOfComments = 0;
            TotalLinesOfEmptySpace = 0;
            MaxLineLength = 0;
        }

        public void AddLineOfCode() => AddLinesOfCode(1);
        
        public void AddLinesOfCode(int lines)
        {
            TotalLinesOfCode += lines;
        }

        public void RemoveLineOfCode() => RemoveLinesOfCode(1);

        public void RemoveLinesOfCode(int lines)
        {
            if ((TotalLinesOfCode - lines) > 0)
                TotalLinesOfCode -= lines;
            else
                throw new InvalidOperationException("Total lines of code is already 0");    
        }

        public void AddLineOfComment() => AddLinesOfComment(1);

        public void AddLinesOfComment(int lines)
        {
            TotalLinesOfComments += lines;
        }

        public void RemoveLineOfComment() => RemoveLinesOfCode(1);

        public void RemoveLinesOfComment(int lines)
        {
            if ((TotalLinesOfComments - lines) > 0)
                TotalLinesOfComments -= lines;
            else
                throw new InvalidOperationException("Total lines of comments is already 0");    
        }

        public void AddLineOfEmptySpace() => AddLinesOfEmptySpace(1);

        public void AddLinesOfEmptySpace(int lines)
        {
            TotalLinesOfEmptySpace += lines;
        }

        public void RemoveLineOfEmptySpace() => RemoveLinesOfEmptySpace(1);

        public void RemoveLinesOfEmptySpace(int lines)
        {
            if ((TotalLinesOfEmptySpace - lines) > 0)
                TotalLinesOfEmptySpace -= lines;
            else
                throw new InvalidOperationException("Total lines of empty spaces is already 0");            
        }

        public int GetTotalLinesOfFile()
        {
            return TotalLinesOfCode + TotalLinesOfComments + TotalLinesOfEmptySpace;
        }

        public void CheckMaxLineLength(string line)
        {
            if (!string.IsNullOrEmpty(line))
                if (line.Trim().Length > MaxLineLength)
                    MaxLineLength = line.Length;
        }

        public int GetMaxLineLength()
        {
            return MaxLineLength;
        }


        public double GetPercentageOfCode(int totalLinesOfCode)
        {
            return CalculatePercentage(totalLinesOfCode, GetTotalLinesOfFile());
        }

        public double GetPercentageOfComments(int totalLinesOfComment)
        {
            return CalculatePercentage(totalLinesOfComment, TotalLinesOfComments);
        }

        public double GetPercentageOfEmptySpace(int totalLinesOfEmptySpace)
        {
            return CalculatePercentage(totalLinesOfEmptySpace, TotalLinesOfEmptySpace);
        }

        public bool IsTheFileWithMaxLineLength(int maxLineLength)
        {
            return maxLineLength == MaxLineLength;
        }

        private double CalculatePercentage(int total, int value)
        {
            double result = 0;

            if (total > 0)
                result = Math.Round((double) value / total * 100, 2);
            else
                result = 0;

            return result;
        }

        public override bool Equals(object obj)
        {
            var fileStatistics = obj as FileStatistics;

            if (fileStatistics != null)
                return false;

            return  fileStatistics.FileName == FileName &&
                    fileStatistics.TotalLinesOfCode == TotalLinesOfCode &&
                    fileStatistics.TotalLinesOfComments == TotalLinesOfComments &&
                    fileStatistics.TotalLinesOfEmptySpace == TotalLinesOfEmptySpace &&
                    fileStatistics.MaxLineLength == MaxLineLength;
        }

        public override int GetHashCode()
        {
           return (GetType().GetHashCode() * 113) + GetHashCode();
        }
    }
}