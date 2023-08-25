using System.Text;
using CodeAnalyzer.App.Enums;
using CodeAnalyzer.App.Factory;
using CodeAnalyzer.App.Interfaces.Output;
using CodeAnalyzer.App.Report;

namespace CodeAnalyzer.App.Output
{
    public class ReportToFile: ReportOutput
    {
        private readonly string _projectPath;
        private readonly string _outputFile;

        public ReportToFile(string projectPath, string outputFile)
        {            
            _projectPath = projectPath;
            _outputFile = outputFile;

            ReportUtil.VerifyProjectFolder(_projectPath);
            ReportUtil.VerifyFileToOutPut(_outputFile);
        }

        public void Generate(Language language)
        {
            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            var result = analyzer.Analyze(_projectPath);

            using (var sr = new StreamWriter(_outputFile, false, Encoding.UTF8))
            {
                var report = new CompleteReport(sr, result.ToList());
                report.GenerateReport();
            }

            Console.WriteLine("Report generated successfully!");
        }
    }
}