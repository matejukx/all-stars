using AllStars.Domain.Dutch.Interfaces;
using AllStars.Domain.User.Interfaces;
using AllStars.Domain.User.Models;

namespace AllStars.Application.Services.User;

public class UserService(IUserRepository userRepository): IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task RegisterUserAsync(AllStarUser user, string password, CancellationToken token)
    {
        var existingUser = await _userRepository.GetOneAsync(user.Nickname, token);
        if (existingUser != null)
        {
            throw new InvalidOperationException("User already exists.");
        }

        var salt = AuthHelpers.GenerateSalt();
        var hashedPassword = AuthHelpers.HashPassword(password, salt);

        user.PasswordSalt = salt;
        user.PasswordHash = hashedPassword;

        await _userRepository.CreateUserAsync(user, token);
    }
}
