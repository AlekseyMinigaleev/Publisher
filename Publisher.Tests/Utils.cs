using Publisher.Exceptions;

namespace Publisher.Tests
{
    internal static class Utils
    {
        public static string GetCurrentProjectDirectoryPath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            var directory = new DirectoryInfo(currentDirectory);

            while (directory is not null && !ContainsSolutionFile(directory))
                directory = directory.Parent;

            return directory?.FullName
            ?? throw new NoSolutionFileException();
        }

        public static string GetOutputDirectoryForTestBuildsPath()
        {
            var solutionFileDirectoryPath = GetCurrentProjectDirectoryPath();
            var directory = new DirectoryInfo(solutionFileDirectoryPath);

            var solutionFileDirectoryPathParent = directory?.Parent?.FullName
                ?? throw new InvalidOperationException("SolutionFileDirectoryPath must have a parent");

            var outputDirectoryForTestBuildsPath = Path.Combine(
                solutionFileDirectoryPathParent,
                "TestBuilds");

            return outputDirectoryForTestBuildsPath;
        }

        private static bool ContainsSolutionFile(DirectoryInfo directory) =>
            directory
                .GetFiles($"*{FileExtensionConstants.SLN}")
                .Length > 0;
    }
}