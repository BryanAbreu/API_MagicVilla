using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupants { get; set; }
        public  int squaremeter { get; set; }
        public string ImageURL { get; set; }
        public string amenidad { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

    }
}
