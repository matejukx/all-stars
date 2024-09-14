using AllStars.Domain.Dutch.Models.Commands;

namespace AllStars.API.DTO.Dutch;

public class CreateDutchGameRequest
{
    public IEnumerable<ScorePair> ScorePairs { get; set; }

    public string Comment { get; set; }
}
