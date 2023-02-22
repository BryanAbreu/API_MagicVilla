using MagicVilla_API.Datos;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbset = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await dbset.AddAsync(entity);
            await Save();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filtro = null, bool traked = true)
        {
            IQueryable<T> query = dbset;
            if (!traked)
            { 
                query = query.AsNoTracking();   
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbset;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();   
        }

        public async Task Remove(T entity)
        {
            dbset.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
