namespace Client.Exceptions;

public class AuthorizationException : Exception
{
    public string[] Errors { get; set; }
    public AuthorizationException(string[] errors) : base()
    {
        Errors = errors;
    }
}
