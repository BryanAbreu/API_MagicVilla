using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository
{
    public class NumVillaRepository : Repository<NumVilla>, INumVillaRepository
    {
        private readonly ApplicationDbContext _context;

        public NumVillaRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<NumVilla> Update(NumVilla entity)
        {
            entity.DateUpdate = DateTime.Now;
            _context.NumVilla.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
