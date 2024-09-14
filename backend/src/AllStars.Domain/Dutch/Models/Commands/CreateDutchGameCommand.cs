namespace AllStars.Domain.Dutch.Models.Commands;

public class CreateDutchGameCommand
{
    public IEnumerable<ScorePair> ScorePairs { get; set; }
    public string Comment { get; set; }

    public override string ToString()
    {
        var scorePairsString = string.Join(", ", ScorePairs.Select(sp => sp.ToString()));
        return $"Comment: {Comment}, ScorePairs: [{scorePairsString}]";
    }
}
