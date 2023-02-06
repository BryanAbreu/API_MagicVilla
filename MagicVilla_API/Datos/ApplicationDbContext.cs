using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options) 
        {
            
        }

        public DbSet<Villa> Villa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Villa Real",
                    Detail = "Detail of villa...",
                    ImageURL = "",
                    Occupants = 10,
                    squaremeter = 150,
                    Rate = 10000,
                    amenidad = "",
                    DateCreate= DateTime.Now,
                    DateUpdate= DateTime.Now


                },
                 new Villa()
                 {
                     Id = 2,
                     Name = "Villa premiun",
                     Detail = "beautifull 3 pool",
                     ImageURL = "",
                     Occupants = 4,
                     squaremeter = 120,
                     Rate = 5240,
                     amenidad = "",
                     DateCreate = DateTime.Now,
                     DateUpdate = DateTime.Now

                 }



                );
        
        }
    }
}
