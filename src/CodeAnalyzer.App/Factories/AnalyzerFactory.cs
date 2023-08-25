using System.ComponentModel;
using CodeAnalyzer.App.Analyzers;
using CodeAnalyzer.App.Enums;
using CodeAnalyzer.App.Interfaces.Analyzer;

namespace CodeAnalyzer.App.Factory
{

    public class AnalyzerFactory
    {
        public static IAnalyzer CreateAnalyzer(Language language)
        {
            switch(language)
            {
                case Language.CSharp:
                    return new CSharpAnalyzer();

                default:
                    throw new InvalidEnumArgumentException("Language not supported");
            }
        }

    }

}