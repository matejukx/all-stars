namespace AllStars.API.DTO.Dutch;

public class PutScoreRequest
{
    public Guid GameId { get; set; }

    public string NickName { get; set; }

    public int Points { get; set; }
}
