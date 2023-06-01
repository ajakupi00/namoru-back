using Core.Entities;
using Core.Models;

namespace Core.Interfaces
{
    public interface INatjecajRepository
    {
        Task<IEnumerable<Natjecaj>> GetListResolvedNatjecaja(Guid id);
        Task<IEnumerable<Natjecaj>> GetFilteredNatjecajs(NatjecajFilter filter);
        Task<Natjecaj> GetNatjecajById(Guid id);

    }
}
