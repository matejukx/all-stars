namespace AllStars.Domain.Dutch.Models.Commands;

public class CreateDutchGameCommand
{
    public IEnumerable<ScorePair> ScorePairs { get; set; }
    public string Comment { get; set; }
}
