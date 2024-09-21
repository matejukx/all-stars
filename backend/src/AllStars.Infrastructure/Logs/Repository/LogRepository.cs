using AllStars.Domain.Dutch.Models;
using AllStars.Domain.Dutch.Models.Commands;
using AllStars.Domain.Logs;
using AllStars.Domain.Logs.Interfaces;
using AllStars.Domain.User.Models;
using System.Data;

namespace AllStars.Infrastructure.Dutch.Repository;

public class LogRepository(AppDbContext context) : ILogRepository
{
    public async Task AddDutchGameCreationLog(CreateDutchGameCommand game, CancellationToken token)
    {
        // get user from context


        //var action = game.ToString();

        //var log = new Log()
        //{
        //    Id = Guid.NewGuid(),
        //    InsertedAt = DateTime.UtcNow,
        //    InsertedBy = user,
        //    LogType = LogType.AddDutchGame,
        //    Action = action
        //};

        //await _context.DutchGames.AddAsync(log, token);
    }
}
