using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Villa> Update(Villa entity)
        {
            entity.DateUpdate = DateTime.Now;
            _context.Villa.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
