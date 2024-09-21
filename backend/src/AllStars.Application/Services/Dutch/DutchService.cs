using AllStars.Domain.Dutch.Interfaces;
using AllStars.Domain.Dutch.Models;
using AllStars.Domain.Dutch.Models.Commands;
using AllStars.Domain.Logs.Interfaces;

namespace AllStars.Application.Services.Dutch;

public class DutchService(IDutchRepository dutchRepository, IUserRepository userRepository, ILogRepository logRepository) : IDutchService
{
    public async Task<IEnumerable<DutchScore>> GetUserScores(string name, CancellationToken token) 
        => await dutchRepository.GetUserScores(name, token);

    public async Task<IEnumerable<DutchScore>> GetAll(CancellationToken token)
        => await dutchRepository.GetAll(token);


    public async Task<bool> UpdateOne(Guid gameId, string nickName, int points, CancellationToken token)
    {
        var score = await dutchRepository.GetOneScore(gameId, nickName, token);
        if (score is null)
        {
            return false;
        }

        await dutchRepository.UpdateOne(score, points, token);

        return true;
    }

    public async Task CreateMany(CreateDutchGameCommand createDutchGameCommand, CancellationToken token)
    {
        var nickNames = createDutchGameCommand.ScorePairs
            .Select(x => x.NickName)
            .ToList();

        var users = await userRepository.GetManyAsync(nickNames, token);

        if (users.Count != nickNames.Count)
        {
            var foundUserNames = users.Select(u => u.Nickname);
            var missingNickNames = nickNames.Except(foundUserNames);

            throw new InvalidOperationException($"Cannot create game. Some users where not found: {string.Join(", ", missingNickNames)}");
        }
        
        var game = new DutchGame
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Comment = createDutchGameCommand.Comment
        };

        var usernames = users.ToDictionary(u => u.Nickname, u => u.Id);

        var scores = createDutchGameCommand
            .ScorePairs
            .GroupBy(sp => sp.Score)
            .OrderBy(g => g.Key)
            .SelectMany((g, i) => g.Select(sp => new DutchScore
            {
                Id = Guid.NewGuid(),
                DutchGameId = game.Id,
                PlayerId = usernames[sp.NickName],
                Points = sp.Score,
                Position = i + 1
            })).ToList();
        
        await dutchRepository.CreateMany(game, scores, token);

        await logRepository.AddDutchGameCreationLog(createDutchGameCommand, token);
    }

    //private async Task<AllStarUser> GetUser(string nickName, CancellationToken token)
    //{
    //    var user = await _userRepository.GetOneAsync(nickName, token);
    //    if (user is null)
    //    {
    //        throw new ApplicationException($"User with name: {nickName} not found. Cannot add Dutch Game.");
    //    }

    //    return user;
    //}

}
