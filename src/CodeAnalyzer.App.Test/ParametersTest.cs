using CodeAnalyzer.App.Output;
using CodeAnalyzer.App.Test;

namespace ToDoList.Application.Test
{
    [Trait("Category", "Parameters")]
    public class ParamatersTest
    {
        [Fact]
        public void ReportConsole_When_InvalidProjectFolder_Throws_Exception()
        {
            Assert.Throws<ArgumentException>(() => new ReportConsole(""));
        }

        [Fact]
        public void ReportToFile_When_InvalidProjectFolder_Throws_Exception()
        {
            Assert.Throws<ArgumentException>(() => new ReportToFile("",Guid.NewGuid().ToString()));
        }

        [Fact]
        public void ReportToFile_When_OutputFileAlreadyExists_Throws_Exception()
        {
            var mock = new FakeData();

            mock.CreateEmptyFile("output.txt");

            Assert.Throws<ArgumentException>(() => new ReportToFile(mock.GetFolderPath(),mock.GetFolderPath() + "output.txt"));
        }
    }
}