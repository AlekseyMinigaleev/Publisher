namespace Publisher.Exceptions
{
    public class IncorrectOutptuDirectoryPath : Exception
    {
        public IncorrectOutptuDirectoryPath(string path) : base($"The specified output directory path is incorrect: '{path}'. " +
               "Please ensure that the path exists, is accessible, and is a valid directory.")
        { }
    }
}