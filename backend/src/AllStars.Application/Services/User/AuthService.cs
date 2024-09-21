using AllStars.Application.Options;
using AllStars.Domain.Dutch.Interfaces;
using AllStars.Domain.User.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AllStars.Application.Services.User;

public class AuthService(IOptions<AllStarsIdentityOptions> options, IUserRepository userRepository) : IAuthService
{
    private readonly AllStarsIdentityOptions _allStarsIdentityOptions = options.Value;

    public async Task<bool> ValidateUserAsync(string nickName, string password, CancellationToken token)
    {
        var user = await userRepository.GetOneAsync(nickName, token);
        return user is not null && AuthHelpers.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
    }

    public string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_allStarsIdentityOptions.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _allStarsIdentityOptions.Issuer,
            audience: _allStarsIdentityOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
