using System.Security.Cryptography;
using System.Text;

namespace AllStars.Application.Services.User;

internal static class AuthHelpers
{
    private const int SaltSize = 16; // 128 bit
    private const int HashSize = 32; // 256 bit
    private const int Iterations = 10000;

    internal static bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
    {
        var hashedInputPassword = HashPassword(inputPassword, storedSalt);
        return hashedInputPassword == storedHash;
    }

    internal static string HashPassword(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, saltBytes, Iterations, HashAlgorithmName.SHA256))
        {
            var hash = rfc2898DeriveBytes.GetBytes(HashSize);
            return Convert.ToBase64String(hash);
        }
    }

    internal static string GenerateSalt()
    {
        var saltBytes = new byte[SaltSize];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }
}
