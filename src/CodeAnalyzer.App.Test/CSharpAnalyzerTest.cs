using System.ComponentModel;
using CodeAnalyzer.App.Enums;
using CodeAnalyzer.App.Factory;
using CodeAnalyzer.App.Reports.Util;
using CodeAnalyzer.App.Test;
using CodeAnalyzer.App.ValueObjects.Common;

namespace ToDoList.Application.Test
{
    [Trait("Category", "CharpAnalyzer")]
    public class CSharpAnalyzerTest 
    {
        [Fact]
        public void CSharpAnalyzer_When_NoFilesToAnalyze_Throws_Exception()
        {
            var fake = new FakeData();

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            Assert.Throws<Exception>(() => analyzer.Analyze(fake.GetFolderPath()));
        }

        [Fact]
        public void CSharpAnalyzer_When_NoFilesToAnalyze_FileExistsInvalidFolder_Throws_Exception()
        {
            var fake = new FakeData();
            fake.CreateFileOnInvalidFolder("test.cs");

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            Assert.Throws<Exception>(() => analyzer.Analyze(fake.GetFolderPath()));
        }

        [Fact]
        public void CSharpAnalyzer_When_CountFilesToAnalyze_Return_True()
        {
            var fake = new FakeData();
            fake.CreateValidFile("FizzBuzz.cs");
            fake.CreateCommentedFile("CommentFile.cs");
            fake.CreateEmptyFile("EmptyFile.cs");
            
            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            var result = analyzer.Analyze(fake.GetFolderPath());

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void CSharpAnalyzer_When_CountFilesToAnalyze_FilesOnInvalidDirectory_Return_True()
        {
            var fake = new FakeData();
            fake.CreateValidFile("FizzBuzz.cs");
            fake.CreateCommentedFile("CommentFile.cs");
            fake.CreateEmptyFile("EmptyFile.cs");
            fake.CreateFileOnInvalidFolder("test.cs");

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            var result = analyzer.Analyze(fake.GetFolderPath());

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void CSharpAnalyzer_When_ValidFile_Return_True()
        {
            var fake = new FakeData();
            fake.CreateValidFile("FizzBuzz.cs");

            var fileResultOk = new FileStatistics(fake.GetFolderPath() + "\\FizzBuzz.cs", 16,7,4,33);

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            var result = analyzer.Analyze(fake.GetFolderPath());

            Assert.NotNull(result);
            Assert.Single(result);            

            var compare = result.FirstOrDefault();
            
            Assert.NotNull(compare);

            Assert.Equal(fileResultOk.TotalLinesOfCode, compare.TotalLinesOfCode);
            Assert.Equal(fileResultOk.TotalLinesOfComments, compare.TotalLinesOfComments);
            Assert.Equal(fileResultOk.TotalLinesOfEmptySpace, compare.TotalLinesOfEmptySpace);
            Assert.Equal(fileResultOk.MaxLineLength, compare.MaxLineLength);
        }

        [Fact]
        public void CSharpAnalyzer_When_CommentedFile_Return_True()
        {
            var fake = new FakeData();
            fake.CreateCommentedFile("commentedfile.cs");

            var fileResultOk = new FileStatistics(fake.GetFolderPath() + "\\commentedfile.cs", 0,3,0,0);

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            var result = analyzer.Analyze(fake.GetFolderPath());

            Assert.NotNull(result);
            Assert.Single(result);            

            var compare = result.FirstOrDefault();
            
            Assert.NotNull(compare);

            Assert.Equal(fileResultOk.TotalLinesOfCode, compare.TotalLinesOfCode);
            Assert.Equal(fileResultOk.TotalLinesOfComments, compare.TotalLinesOfComments);
            Assert.Equal(fileResultOk.TotalLinesOfEmptySpace, compare.TotalLinesOfEmptySpace);
            Assert.Equal(fileResultOk.MaxLineLength, compare.MaxLineLength);
        }
        [Fact]
        public void CSharpAnalyzer_When_ErrorCommentedFile_Return_Exception()
        {
            var fake = new FakeData();
            fake.CreateInvalidFile("invalid.cs");

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            Assert.Throws<Exception>(() => analyzer.Analyze(fake.GetFolderPath()));
        }

        [Fact]
        public void CSharpAnalyzer_When_EmptyFile_Return_True()
        {
            var fake = new FakeData();
            fake.CreateEmptyFile("emptyfile.cs");

            var fileResultOk = new FileStatistics(fake.GetFolderPath() + "\\emptyfile.cs", 0,0,0,0);

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            var result = analyzer.Analyze(fake.GetFolderPath());

            Assert.NotNull(result);
            Assert.Single(result);            

            var compare = result.FirstOrDefault();
            
            Assert.NotNull(compare);

            Assert.Equal(fileResultOk.TotalLinesOfCode, compare.TotalLinesOfCode);
            Assert.Equal(fileResultOk.TotalLinesOfComments, compare.TotalLinesOfComments);
            Assert.Equal(fileResultOk.TotalLinesOfEmptySpace, compare.TotalLinesOfEmptySpace);
            Assert.Equal(fileResultOk.MaxLineLength, compare.MaxLineLength);
        }

        [Fact]
        public void CSharpAnalyzer_When_ProjectStatisticsIsOk_Return_True()
        {
            var fake = new FakeData();
            
            var listOfData = new List<FileStatistics>();

            listOfData.Add(fake.CreateValidFile("FizzBuzz1.cs")); 
            listOfData.Add(fake.CreateValidFile("FizzBuzz2.cs")); 
            listOfData.Add(fake.CreateValidFile("FizzBuzz3.cs")); 
            listOfData.Add(fake.CreateCommentedFile("CommentFile.cs")); 
            listOfData.Add(fake.CreateEmptyFile("Emptyfile.cs")); 

            fake.CreateFileOnInvalidFolder("teste.cs"); // do not consider this file for calculation!

            var projectStatistics = new ProjectStatistics(
                listOfData.Sum(x => x.TotalLinesOfCode),
                listOfData.Sum(x => x.TotalLinesOfComments),
                listOfData.Sum(x => x.TotalLinesOfEmptySpace),
                listOfData.Max(x => x.MaxLineLength)
            );

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            var _fileStatistics = analyzer.Analyze(fake.GetFolderPath());

            var _calculate = new FileStatisticsCalculate(_fileStatistics.ToList());
            var _fileStatisticsResume = _calculate.CalculateFileStatistics();

            Assert.NotNull(_fileStatisticsResume);

            Assert.Equal(projectStatistics.TotalLinesOfCode, _fileStatisticsResume.ProjectStatistics.TotalLinesOfCode);
            Assert.Equal(projectStatistics.TotalLinesOfComments, _fileStatisticsResume.ProjectStatistics.TotalLinesOfComments);
            Assert.Equal(projectStatistics.TotalLinesOfEmptySpace, _fileStatisticsResume.ProjectStatistics.TotalLinesOfEmptySpace);
            Assert.Equal(projectStatistics.MaxLineLength, _fileStatisticsResume.ProjectStatistics.MaxLineLength);

        }

        [Fact]
        public void CSharpAnalyzer_When_ProjectContainsAnInvalidFile_Throws_Exception()
        {
            var fake = new FakeData();
            
            var listOfData = new List<FileStatistics>();

            fake.CreateValidFile("FizzBuzz1.cs"); 
            fake.CreateValidFile("FizzBuzz2.cs"); 
            fake.CreateValidFile("FizzBuzz3.cs"); 
            fake.CreateCommentedFile("CommentFile.cs"); 
            fake.CreateEmptyFile("Emptyfile.cs"); 
            fake.CreateInvalidFile("InvalidFile.cs"); 

            var analyzer = AnalyzerFactory.CreateAnalyzer(Language.CSharp);
            Assert.Throws<Exception>(() => analyzer.Analyze(fake.GetFolderPath()));
        }

    }
}