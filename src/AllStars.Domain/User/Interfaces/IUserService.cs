namespace AllStars.Domain.User.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(string username);

    Task<bool> ValidateUserAsync(string nickName, string password, CancellationToken token);
}
