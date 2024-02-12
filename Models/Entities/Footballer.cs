using System.ComponentModel.DataAnnotations;
using WebFootballers.Models.Utility;

namespace WebFootballers.Models.Entities
{
    public class Footballer
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public bool IsMale { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public FootballTeam Team { get; set; } = null!;
        public Country Country { get; set; }

    }
}
