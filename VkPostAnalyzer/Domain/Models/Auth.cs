namespace Domain.Models;

public class Auth
{
    public long Id { get; set; }
    public string State { get; set; }
    public string CodeVerifier { get; set; }

    public Auth(string state, string codeVerifier)
    {
        State = state;
        CodeVerifier = codeVerifier;
    }
}