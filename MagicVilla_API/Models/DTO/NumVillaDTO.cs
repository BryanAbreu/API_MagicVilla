using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class NumVillaDTO
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int Villaid { get; set; }

        public string EspecialDetails { get; set; }
       


    }
}
