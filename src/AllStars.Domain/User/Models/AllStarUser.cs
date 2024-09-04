using AllStars.Domain.Dutch.Models;

namespace AllStars.Domain.User.Models;

public class AllStarUser
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Nickname { get; set; }

    public DateTime? BirthDate { get; set; }

    public Families Families { get; set; }

    public ICollection<DutchScore> DutchScores { get; set; } = new List<DutchScore>();
}
