using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFootballers.Models.Entities;
using WebFootballers.Models.Utility;

namespace WebFootballers.Models.DTOs
{
    public class FootballerCreationDTO
    {
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