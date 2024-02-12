using System.ComponentModel.DataAnnotations;
using WebFootballers.Models.Utility;

namespace WebFootballers.Models.DTOs
{
    public class FootballerDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть не менее 1")]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string IsMale { get; set; } = null!;
        [Required]
        public string Dob { get; set; } = null!;
        [Required]
        public string Team { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
    }
}
