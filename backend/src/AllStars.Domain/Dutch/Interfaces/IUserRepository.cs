using AllStars.Domain.User.Models;

namespace AllStars.Domain.Dutch.Interfaces;

public interface IUserRepository
{
    Task<AllStarUser?> GetOneAsync(string nickName, CancellationToken token);

    Task<IEnumerable<AllStarUser>> GetManyAsync(IEnumerable<string> nickNames, CancellationToken token);

    Task CreateDefaultUsersAsync(CancellationToken token);

    Task CreateUserAsync(AllStarUser user, CancellationToken token);
}
