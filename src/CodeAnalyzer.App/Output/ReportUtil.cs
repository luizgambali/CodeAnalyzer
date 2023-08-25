using CodeAnalyzer.App.ValueObjects.Common;

namespace CodeAnalyzer.App.Output
{
    public class ReportUtil
    {
        public static void VerifyProjectFolder(string projectFolder)
        {
            if (string.IsNullOrEmpty(projectFolder) || !Directory.Exists(projectFolder))
                throw new ArgumentException("Project folder does not exist");
        }

        public static void VerifyFileToOutPut(string fileOutput)
        {
            if (string.IsNullOrEmpty(fileOutput) || File.Exists(fileOutput))
                throw new ArgumentException($"Output file already exists: {fileOutput}");
        }
    }
}