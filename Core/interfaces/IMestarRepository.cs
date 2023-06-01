using Core.Entities;
using Core.Models;

namespace Core.interfaces
{
    public interface IMestarRepository
    {
        Task<IEnumerable<Mestar>> Search(MestarFilter search);
    }
}
