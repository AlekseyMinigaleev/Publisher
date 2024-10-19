namespace Publisher.Tests
{
    internal static class DirectoryPathsForPublisherCreationTests
    {
        private static string? _testBuildsDirectoryPath;

        public static string TestBuildsDirectoryPath
        {
            get
            {
                _testBuildsDirectoryPath ??= Utils.GetTestBuildsDirectoryPath();

                return _testBuildsDirectoryPath;
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