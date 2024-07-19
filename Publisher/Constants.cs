namespace Publisher
{
    public static class PlatformConstants
    {
        public const string WINDOWS_X64 = "win-x64";
        public const string LINUX_X64 = "linux-x64";
    }

    public static class FileExtensionConstants
    {
        public const string CJPROJ = ".csproj";
        public const string SLN = ".sln";
    }

    public static class DirectoryNameConstants
    {
        public const string NEW_FOLDER = "new folder";
        public const string EXIST_EMPTY_OUTPUT = "EmptyOutputDirectory";
        public const string BUILDS = "Builds";
        public const string SOLUTION_FOR_TEST_PUBLISHER = "SolutionForTestPublisher";
    }

    public static class CsprojLinesConstants 
    {
        public const string PROJECT = "Project(";
        public const string UseWPF_TAG = "<UseWPF>";
    }
}
