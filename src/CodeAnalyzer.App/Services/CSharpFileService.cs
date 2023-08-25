using CodeAnalyzer.App.Interfaces.Service;

namespace CodeAnalyzer.App.Services
{
    public class CSharpFileService : IFileService
    {
        public List<string> GetFilesFromParentDirectory(string parentDirectory)
        {
            var result = GetFilesFromDirectory(parentDirectory);

            RemoveUnnecessaryFiles(result);

            return result;
        }

        private List<string> GetFilesFromDirectory(string directory)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(directory) || Directory.Exists(directory) == false)
                throw new ArgumentException("Invalid directory");

            var _parentDirectory = Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories);

            foreach (var file in _parentDirectory)
                result.Add(file);

            return result;
        }

        private void RemoveUnnecessaryFiles(List<string> files)
        {
            var slash = Environment.OSVersion.Platform == PlatformID.Win32NT ? "\\" : "/";

            files.RemoveAll(file => file.Contains(slash + ".vs"));
            files.RemoveAll(file => file.Contains(slash + ".vscode"));
            files.RemoveAll(file => file.Contains(slash + ".git"));
            files.RemoveAll(file => file.Contains(slash + "Properties"));
            files.RemoveAll(file => file.Contains(slash + "obj" + slash));
            files.RemoveAll(file => file.Contains(slash + "bin"+ slash));            
        }
    }
}