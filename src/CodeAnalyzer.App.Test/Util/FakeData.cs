using System;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using CodeAnalyzer.App.Entities;

namespace CodeAnalyzer.App.Test
{

    public class ValidFile
    {
        public static int TotalLinesOfCode = 16;
        public static int TotalLinesOfComments = 7;
        public static int TotalLinesOfEmptySpace = 4;
        public static int MaxLineLength = 37;

        public static string GetFileCode(){

            var fileContent = new StringBuilder();

            fileContent.AppendLine("/*");
            fileContent.AppendLine("Este é um arquivo válido, que contém comentários, espaços em branco e linhas de código");
            fileContent.AppendLine("*/");
            fileContent.AppendLine("namespace MockTest.File");
            fileContent.AppendLine("{");
            fileContent.AppendLine("    public class TestFile");
            fileContent.AppendLine("    {");
            fileContent.AppendLine("");
            fileContent.AppendLine("        public string FizzBuzz(int value)");
            fileContent.AppendLine("        {");
            fileContent.AppendLine("            //se for divisivel por 3 e 5 retorna FizzBuzz");
            fileContent.AppendLine("            if (value % 3 == 0 && value % 5 == 0)");
            fileContent.AppendLine("                return \"FizzBuzz\";");
            fileContent.AppendLine("");
            fileContent.AppendLine("            //se for divisivel por 3 retorna Fizz");
            fileContent.AppendLine("            if (value % 3 == 0)");
            fileContent.AppendLine("                return \"Fizz\";");
            fileContent.AppendLine("");
            fileContent.AppendLine("            //se for divisivel por 5 retorna Buzz");
            fileContent.AppendLine("            if (value % 5 == 0)");
            fileContent.AppendLine("                return \"Buzz\";");
            fileContent.AppendLine("");
            fileContent.AppendLine("            return value.ToString();");
            fileContent.AppendLine("        }");
            fileContent.AppendLine("    }");
            fileContent.AppendLine("}");
            fileContent.AppendLine("/* este é mais um comentário, de uma unica linha*/");

            return fileContent.ToString();
        }
    }

    public class CommentedFile
    {
        public static int TotalLinesOfCode = 0;
        public static int TotalLinesOfComments = 3;
        public static int TotalLinesOfEmptySpace = 0;
        public static int MaxLineLength = 0;

        public static string GetFileCode()
        {
            var fileContent = new StringBuilder();

            fileContent.AppendLine("/*");
            fileContent.AppendLine(" Este é um arquivo totalmente comentado!");
            fileContent.AppendLine("*/");

            return fileContent.ToString();
        }
    }

    public class FakeData: IDisposable
    {
        private readonly string _folderPath;
        private readonly string slash;

        public FakeData()
        {
            slash = Environment.OSVersion.Platform == PlatformID.Win32NT ? "\\" : "/";
            
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                _folderPath = $"{Environment.SpecialFolder.UserProfile}{slash}CodeAnalyzerTestFolder{slash}{Guid.NewGuid()}{slash}";
            else
                _folderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{slash}CodeAnalyzerTestFolder{slash}{Guid.NewGuid()}{slash}";
                
            ClearFolder();

            CreateFolder(_folderPath);
            CreateFolder(_folderPath + "obj");
            CreateFolder(_folderPath + "bin");
            CreateFolder(_folderPath + "Properties");
        }

        public string GetFolderPath()
        {
            return _folderPath;
        }

        private void CreateFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
        private void ClearFolder()
        {
            if (Directory.Exists(_folderPath))
            {
                Directory.Delete(_folderPath, true);
            }
        }

        public FileStatistics CreateEmptyFile(string fileName)
        {
            CreateFile(fileName, string.Empty);

            return new FileStatistics(_folderPath + fileName);
        }

        public FileStatistics CreateCommentedFile(string fileName)
        {
            CreateFile(fileName, CommentedFile.GetFileCode());

            var fs = new FileStatistics(_folderPath + fileName,CommentedFile.TotalLinesOfCode,
                                                               CommentedFile.TotalLinesOfComments,
                                                               CommentedFile.TotalLinesOfEmptySpace,
                                                               CommentedFile.MaxLineLength);

            return fs;
        }

        public void CreateInvalidFile(string fileName)
        {
            var fileContent = new StringBuilder();
            fileContent.AppendLine("/*");
            fileContent.AppendLine(" Este é um arquivo inválido, porque não fecha o comentario!");

            CreateFile(fileName, fileContent.ToString());
        }

        public void CreateFileOnInvalidFolder(string fileName)
        {
            CreateValidFile("obj" + slash + fileName); 
            CreateValidFile("bin" + slash + fileName); 
            CreateValidFile("Properties" + slash + fileName);
        }

        public FileStatistics CreateValidFile(string fileName)
        {
            CreateFile(fileName, ValidFile.GetFileCode());

            var fs = new FileStatistics(_folderPath + fileName, ValidFile.TotalLinesOfCode,
                                                                ValidFile.TotalLinesOfComments,
                                                                ValidFile.TotalLinesOfEmptySpace,
                                                                ValidFile.MaxLineLength);
            
            return fs;
        }

        private void CreateFile(string fileName, string content)
        {
            using(StreamWriter sr = new StreamWriter(_folderPath + fileName))
            {
                sr.Write(content);
                sr.Flush();
            }
        }

        public void Dispose()
        {
            ClearFolder();
        }
    }
}