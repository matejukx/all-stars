namespace AllStars.Domain.Dutch.Models;

public class DutchGame
{
    public Guid Id { get; set; }

    public DateTime? Date { get; set; }

    public string Comment { get; set; }

    public ICollection<DutchScore> DutchScores { get; set; } = new List<DutchScore>();
}
