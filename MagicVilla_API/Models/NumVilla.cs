using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class NumVilla
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }

        [Required]
        public int Villaid { get; set;}

        [ForeignKey("Villaid")]
        public Villa villa { get; set;}

        public string EspecialDetails { get; set;}
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
