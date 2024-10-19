namespace Publisher.Tests
{
    internal static class DirectoryPathsForPublisherCreationTests
    {
        private static string? _outputDirectoryForTestBuilds;

        public static string OutputDirectoryForTestBuilds
        {
            get
            {
                _outputDirectoryForTestBuilds ??= Utils.GetOutputDirectoryForTestBuildsPath();

                return _outputDirectoryForTestBuilds;
            }
        }

        private static string? _currentProjectDirectoryPath;

        public static string CurrentProjectDirectoryPath
        {
            get
            {
                _currentProjectDirectoryPath ??= Utils.GetCurrentProjectDirectoryPath();

                return _currentProjectDirectoryPath;
            }
        }
    }
}