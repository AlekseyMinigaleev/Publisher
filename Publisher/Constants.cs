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

        public const string EXIST_EMPTY_OUTPUT = "Empty";

        public const string BUILDS = "Builds";

        public const string ALL_PROJECT_SOLUTION = "AllProjectSolution";

        public const string WPF_SOLUTION = "WpfProjectSolution";
    }

    public static class CsprojLinesConstants
    {
        public const string PROJECT = "Project(";

        public const string UseWPF_TAG = "<UseWPF>";
    }

    public static class DirectoryPathConstatns
    {
        public static readonly string BASE_PATH = Utils.GetBaseDirectory();

        public static readonly string ALL_PROJECT_SOLUTION = $"{BASE_PATH}\\{DirectoryNameConstants.ALL_PROJECT_SOLUTION}";

        public static readonly string WPF_PROJECT_SOLUTION = $"{BASE_PATH}\\{DirectoryNameConstants.WPF_SOLUTION}";

        public static readonly string BUILDS = $"{BASE_PATH}\\{DirectoryNameConstants.BUILDS}";

        public static readonly string NOT_EXIST_OUTPUT = $"{BASE_PATH}\\{DirectoryNameConstants.NEW_FOLDER}";

        public static readonly string EXIST_EMPTY_OUTPUT = $"{BASE_PATH}\\{DirectoryNameConstants.EXIST_EMPTY_OUTPUT}";
    }
}