using AllStars.Domain.Dutch.Models.Commands;
using AllStars.Domain.User.Models;

namespace AllStars.Domain.Logs.Interfaces;

public interface ILogRepository
{
    Task AddDutchGameCreationLog(CreateDutchGameCommand game, CancellationToken token);
}
