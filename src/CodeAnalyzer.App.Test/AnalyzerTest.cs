using System.ComponentModel;
using CodeAnalyzer.App.Enums;
using CodeAnalyzer.App.Factory;

namespace ToDoList.Application.Test
{
    [Trait("Category", "Analyzer")]
    public class AnalyzerTest 
    {

        [Fact]
        public void Analyzer_When_UnsupportedLanguage_Throws_Exception()
        {
            Assert.Throws<InvalidEnumArgumentException>(() => AnalyzerFactory.CreateAnalyzer((Language)25));            
        }

        [Fact]
        public void Analyzer_When_InvalidProjectDirectory_Throws_Exception()
        {
            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            Assert.Throws<ArgumentException>(() => analyzer.Analyze(""));            
        }
    }
}