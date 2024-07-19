namespace Publisher
{
    public class Utils
    {
        public static string GetBaseDirectory()
        {
            string relativePath = @"..\..\..\..\..\";

            string solutionDirectory = Path
                .GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(),
                    relativePath));

            var result = $"" +
                $"{solutionDirectory}";

            return result;
        }
    }
}
