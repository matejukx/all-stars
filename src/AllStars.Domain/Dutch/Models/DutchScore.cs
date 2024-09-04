using AllStars.Domain.User.Models;

namespace AllStars.Domain.Dutch.Models;

public class DutchScore
{
    public Guid Id { get; set; }

    public int Points { get; set; }

    public Guid DutchGameId { get; set; }
    public DutchGame Game { get; set; }

    public Guid PlayerId { get; set; }
    public AllStarUser Player { get; set; }
}
