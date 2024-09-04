using AllStars.Domain.Dutch.Models.Commands;
using AllStars.Domain.Dutch.Models;

namespace AllStars.Domain.Dutch.Interfaces;

public interface IDutchService
{
    Task<IEnumerable<DutchScore>> GetUserScores(string name, CancellationToken token);

    Task<IEnumerable<DutchScore>> GetAll(CancellationToken token);

    Task<bool> UpdateOne(Guid gameId, string nickName, int points, CancellationToken token);

    Task CreateMany(CreateDutchGameCommand createDutchGameCommand, CancellationToken token);
}
