using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Detail { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupants { get; set; }
        public int squaremeter { get; set; }
        public string ImageURL { get; set; }
        public string amenidad { get; set; }
    }
}
