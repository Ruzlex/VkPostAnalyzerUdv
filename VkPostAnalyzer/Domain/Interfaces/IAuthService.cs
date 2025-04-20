namespace Domain.Interfaces;

public interface IAuthService
{
    Task<string> GenerateUrlForAuth();
    Task<string?> AuthResponseProcessing(string code, string deviceId, string state);
}