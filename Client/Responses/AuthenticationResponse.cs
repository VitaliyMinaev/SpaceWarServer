namespace Client.Responses;

internal class AuthenticationResponse
{
    public string access_token { get; set; }
    public string[] errors { get; set; }
}
