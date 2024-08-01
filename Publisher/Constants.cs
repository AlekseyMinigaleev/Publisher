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

        public const string BUILDS = "Builds";

        public const string ALL_PROJECT_SOLUTION = "AllProjectSolution";

        public const string WPF_SOLUTION = "WpfProjectSolution";
    }

    public static class CsprojLinesConstants
    {
        public const string PROJECT = "Project(";
    }

    public static class DirectoryPathConstatns
    {
        public static readonly string BASE_PATH = Utils.GetBaseDirectory();

        public static readonly string ALL_PROJECT_SOLUTION = $"{BASE_PATH}{DirectoryNameConstants.ALL_PROJECT_SOLUTION}";

        public static readonly string WPF_PROJECT_SOLUTION = $"{BASE_PATH}{DirectoryNameConstants.WPF_SOLUTION}";

        public static readonly string BUILDS = $"{BASE_PATH}{DirectoryNameConstants.BUILDS}";

        public static readonly string OUTPUT = $"{BASE_PATH}{DirectoryNameConstants.BUILDS}";
    }
}