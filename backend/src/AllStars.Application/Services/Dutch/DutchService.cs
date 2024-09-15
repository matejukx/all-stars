using AllStars.Domain.Dutch.Interfaces;
using AllStars.Domain.Dutch.Models;
using AllStars.Domain.Dutch.Models.Commands;
using AllStars.Domain.Logs.Interfaces;

namespace AllStars.Application.Services.Dutch;

public class DutchService(IDutchRepository dutchRepository, IUserRepository userRepository, ILogRepository logRepository) : IDutchService
{
    private readonly IDutchRepository _dutchRepository = dutchRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogRepository _logRepository = logRepository;

    public async Task<IEnumerable<DutchScore>> GetUserScores(string name, CancellationToken token)
    {
        var scores = await _dutchRepository.GetUserScores(name, token);
        return scores;
    }

    public async Task<IEnumerable<DutchScore>> GetAll(CancellationToken token)
    {
        var scores = await _dutchRepository.GetAll(token);
        return scores;
    }

    public async Task<bool> UpdateOne(Guid gameId, string nickName, int points, CancellationToken token)
    {
        var score = await _dutchRepository.GetOneScore(gameId, nickName, token);
        if (score is null)
        {
            return false;
        }

        await _dutchRepository.UpdateOne(score, points, token);


        return true;
    }

    public async Task CreateMany(CreateDutchGameCommand createDutchGameCommand, CancellationToken token)
    {
        var nickNames = createDutchGameCommand.ScorePairs
            .Select(x => x.NickName);

        var users = await _userRepository.GetManyAsync(nickNames, token);

        if (users.Count() != nickNames.Count())
        {
            var foundUsers = users.Select(u => u.Nickname);
            var missingNickNames = nickNames.Except(foundUsers);
            var missingNickNamesMessage = string.Join(", ", missingNickNames);

            throw new InvalidOperationException($"Cannot create game. Some users where not found: {missingNickNamesMessage}");
        }

        var game = new DutchGame()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Comment = createDutchGameCommand.Comment
        };

        var sortedScorePairs = createDutchGameCommand.ScorePairs
            .OrderBy(c => c.Score)
            .ToList();

        var scores = new List<DutchScore>();
        int currentRank = 1;
        int previousScore = int.MinValue;
        int currentRankStartIndex = 0;

        for (int i = 0; i < sortedScorePairs.Count; i++)
        {
            var scorePair = sortedScorePairs[i];

            if (scorePair.Score != previousScore)
            {
                currentRank = i + 1; 
                previousScore = scorePair.Score;
                currentRankStartIndex = i;
            }

            scores.Add(new DutchScore
            {
                Id = Guid.NewGuid(),
                DutchGameId = game.Id,
                PlayerId = users.Single(u => u.Nickname == scorePair.NickName).Id,
                Points = scorePair.Score,
                Position = currentRank
            });
        }

        await _dutchRepository.CreateMany(game, scores, token);

        await _logRepository.AddDutchGameCreationLog(createDutchGameCommand, token);
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
