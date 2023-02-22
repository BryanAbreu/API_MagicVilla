using MagicVilla_API.Models;

namespace MagicVilla_API.Repository.IRepository
{
    public interface INumVillaRepository: IRepository<NumVilla>
    {
        Task<NumVilla> Update(NumVilla villa);
    }
}
