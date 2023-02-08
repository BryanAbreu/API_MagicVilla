using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Detail { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Occupants { get; set; }
        [Required]
        public int squaremeter { get; set; }
        [Required]
        public string ImageURL { get; set; }
        public string amenidad { get; set; }
    }
}
