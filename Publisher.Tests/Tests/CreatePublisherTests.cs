namespace Publisher.Tests.Tests
{
    public class CreatePublisherTests
    {
        //[Fact]
        //public void Wrong_SolutionDirectory() =>
        //    Assert.Throws<NoSolutionFileException>(
        //        () => new Publisher(
        //            $"{Directory.GetCurrentDirectory()}",
        //            DirectoryPathConstatns.BUILDS,
        //            "1"));

        //[Fact]
        //public void Rigth_SolutionDirectory()
        //{
        //    var publisher = new Publisher(
        //        DirectoryPathConstatns.ALL_PROJECT_SOLUTION,
        //        DirectoryPathConstatns.BUILDS,
        //        "1");

        //    var slnExtension = Path
        //        .GetExtension(publisher.SolutionFilePath);

        //    Assert.Equal($"{FileExtensionConstants.SLN}", slnExtension);
        //}

        [Fact]
        public void Exist_OutputDirectory()
        {
            if (!Directory.Exists(DirectoryPathsForPublisherCreationTests.TestBuildsDirectoryPath))
                Directory.CreateDirectory(DirectoryPathsForPublisherCreationTests.TestBuildsDirectoryPath);

            Publisher? publisher = null;

            var exception = Record.Exception(() =>
                publisher = new Publisher(
                    DirectoryPathsForPublisherCreationTests.CurrentProjectDirectoryPath,
                    DirectoryPathsForPublisherCreationTests.TestBuildsDirectoryPath,
                    "1"));

            Assert.Null(exception);
            Assert.NotNull(publisher);
        }

        //[Fact]
        //public void Not_Exist_OutputDirectory()
        //{
        //    Publisher? publisher = null;

        //    var exception = Record.Exception(() =>
        //        publisher = new Publisher(
        //            DirectoryPathConstatns.ALL_PROJECT_SOLUTION,
        //            $"{DirectoryPathConstatns.BASE_PATH}foo\\bar\\bazz",
        //            "1"));

        //    Assert.Null(exception);
        //    Assert.NotNull(publisher);
        //}
    }
}