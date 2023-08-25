using CodeAnalyzer.App.Enums;

namespace CodeAnalyzer.App.Interfaces.Output
{
    public interface ReportOutput
    {
        void Generate(Language language);
    }
}