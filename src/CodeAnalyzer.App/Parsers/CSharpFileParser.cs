using CodeAnalyzer.App.Interfaces.Parser;
using CodeAnalyzer.App.ValueObjects.Common;

namespace CodeAnalyzer.App.Parser
{
    public class CSharpFileParser: IFileParser
    {
        public FileStatistics Parse(string filePath)
        {
            var result = new FileStatistics(filePath);

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = "";

                while((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (IsCommentBlockStart(line) && ContainsCommentBlockEnd(line))
                    {
                        result.AddLineOfComment();
                    }
                    else 
                    {
                        if (IsCommentBlockStart(line))
                        {
                            result.AddLineOfComment();

                            while (!ContainsCommentBlockEnd(line))
                            {
                                line = sr.ReadLine();

                                if (string.IsNullOrEmpty(line))
                                    throw new Exception("The file possible contains a comment block not closed");

                                result.AddLineOfComment();
                            }
                        }    
                        else if(IsEmptyLine(line))
                        {
                            result.AddLineOfEmptySpace();
                        }
                        else if(IsCommentLine(line))
                        {
                            result.AddLineOfComment();
                        }
                        else if(IsCommentBlock(line))
                        {
                            result.AddLineOfComment();
                        }
                        else
                        {
                            result.CheckMaxLineLength(line);
                            result.AddLineOfCode();
                        }
                    }
                }
            }

            return result;
        }

        private bool IsEmptyLine(string line)
        {
            return string.IsNullOrWhiteSpace(line);
        }

        private bool IsCommentLine(string line)
        {
            return line.TrimStart().StartsWith("//");
        }
        private bool IsCommentBlockStart(string line)
        {
            return line.TrimStart().StartsWith("/*");
        }
        private bool IsCommentBlockEnd(string line)
        {
            return line.TrimStart().StartsWith("*/");
        }
        private bool ContainsCommentBlockEnd(string line)
        {
            return line.EndsWith("*/");
        }

        private bool IsCommentBlock(string line)
        {
            return IsCommentBlockStart(line) || IsCommentBlockEnd(line);
        }
    }
}