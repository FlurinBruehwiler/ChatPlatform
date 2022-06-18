namespace ChatPlatformBackend.Exceptions;

public class BadRequestException : Exception
{
    public Error Error { get; }

    public BadRequestException(Error error)
    {
        Error = error;
    }
}