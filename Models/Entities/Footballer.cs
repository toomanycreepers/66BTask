using WebFootballers.Models.Utility;

namespace WebFootballers.Models.Entities
{
    public class Footballer : BaseEntity
    {
        public string Surname { get; set; } = null!;
        public bool IsMale { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public FootballTeam Team { get; set; } = null!;
        public Country Country { get; set; }

    }
}
