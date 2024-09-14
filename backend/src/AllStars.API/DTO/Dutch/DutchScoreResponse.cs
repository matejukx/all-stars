namespace AllStars.API.DTO.Dutch;

public class DutchScoreResponse
{
    public Guid Id { get; set; }
    public int Points { get; set; }
    public Guid DutchGameId { get; set; }
    public string Nickname { get; set; }
}
