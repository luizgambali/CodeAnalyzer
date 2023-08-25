using CodeAnalyzer.App.Interfaces.Analyzer;
using CodeAnalyzer.App.Parser;
using CodeAnalyzer.App.Services;
using CodeAnalyzer.App.ValueObjects.Common;

namespace CodeAnalyzer.App.Analyzers
{
    public class CSharpAnalyzer : IAnalyzer
    {
        public IEnumerable<FileStatistics> Analyze(string parentDirectory)
        {
            var  fileStatistics = new List<FileStatistics>();
            
            var files = new CSharpFileService().GetFilesFromParentDirectory(parentDirectory);
            var fileParser = new CSharpFileParser();

            foreach(string file in files)
                fileStatistics.Add(fileParser.Parse(file)); 

            if (fileStatistics.Count == 0)
                throw new Exception("No files to analyze.");
                
            return fileStatistics;                       
        }
    }
}