namespace Publisher.Tests
{
    internal static class Utils
    {
        public static string GetLocalSolutionDirectory()
        {
            string relativePath = @"..\..\..\..\..\";

            string solutionDirectory = Path
                .GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(),
                    relativePath));

            return $"" +
                $"{solutionDirectory}" +
                $"\\{DirectoryNameConstants.SOLUTION_FOR_TEST_PUBLISHER}";
        }
    }
}
