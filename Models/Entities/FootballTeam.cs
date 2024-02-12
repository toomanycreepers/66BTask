using System.ComponentModel.DataAnnotations;

namespace WebFootballers.Models.Entities
{
    public class FootballTeam
    { 
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
