using AllStars.Domain.Dutch.Interfaces;
using AllStars.Domain.Dutch.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;

namespace AllStars.Infrastructure.Dutch.Repository;

public class DutchRepository : IDutchRepository
{
    private readonly AppDbContext _context;

    public DutchRepository(AppDbContext context) => _context = context;

    public async Task CreateMany(DutchGame game, IEnumerable<DutchScore> scores, CancellationToken token)
    {
        //if (scores.Any(s => s.Id != game.Id))
        //{
        //    throw new DataException("Could not insert Dutch Games. Id is not consistient.");
        //}

        await _context.DutchGames.AddAsync(game, token);
        await _context.DutchScores.AddRangeAsync(scores, token);        
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateOne(DutchScore score, int points, CancellationToken token)
    {
        score.Points = points;

        await _context.SaveChangesAsync(token);
    }


    public async Task<IEnumerable<DutchScore>> GetAll(CancellationToken token)
    {
        return await _context.DutchScores
            .Include(ds => ds.Player)
            .Include(ds => ds.Player)
            .ToListAsync(token);
    }

    public async Task<IEnumerable<DutchScore>> GetUserScores(string nickName, CancellationToken token)
    {
        return await _context.DutchScores
            .Where(ds => ds.Player.Nickname == nickName)
            .Include(ds => ds.Player)
            .Include(ds => ds.Game)
            .ToListAsync(token);
    }

    public async Task<DutchScore?> GetOneScore(Guid gameId, string nickName, CancellationToken token)
    {
        return await _context.DutchScores
          .Include(ds => ds.Game) // check if neccessery
          .Include(ds => ds.Player) // check if neccessery
          .Where(ds => ds.Game.Id == gameId)
          .Where(ds => ds.Player.Nickname == nickName)
          .SingleOrDefaultAsync(token);
    }
}
