namespace Publisher.Exceptions
{
    public class ProcessExitCodeException : Exception
    {
        public ProcessExitCodeException(string name, int exitCode)
            : base($"`{name}` has exited with code {exitCode}")
        { }
    }
}