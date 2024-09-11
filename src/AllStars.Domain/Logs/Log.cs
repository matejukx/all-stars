using AllStars.Domain.User.Models;

namespace AllStars.Domain.Logs;

public class Log
{
    public Guid Id { get; set; }

    public string Action { get; set; }

    public LogType LogType { get; set; }

    public AllStarUser InsertedBy { get; set; }

    public DateTime InsertedAt { get; set; }
}
