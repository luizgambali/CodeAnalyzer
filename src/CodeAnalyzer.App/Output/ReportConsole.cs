using CodeAnalyzer.App.Enums;
using CodeAnalyzer.App.Factory;
using CodeAnalyzer.App.Interfaces.Output;
using CodeAnalyzer.App.Report;

namespace CodeAnalyzer.App.Output
{
    public class ReportConsole: ReportOutput
    {
        private readonly string _projectPath;

        public ReportConsole(string projectPath)
        {            
            _projectPath = projectPath;

            ReportUtil.VerifyProjectFolder(_projectPath);
        }

        public void Generate(Language language)
        {
            var analyzer = AnalyzerFactory.CreateAnalyzer(language);
            var result = analyzer.Analyze(_projectPath);

            var report = new CompleteReport(new StreamWriter(Console.OpenStandardOutput()), result.ToList());
            report.GenerateReport();  
        }
    }
}