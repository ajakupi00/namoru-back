using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NatjecajRepository : INatjecajRepository
    {
        private readonly ApplicationContext _context;
        public NatjecajRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Natjecaj>> GetListResolvedNatjecaja(Guid userID)
        {
            return await _context.Natjecaji.AsNoTracking().Where(x => x.UserID == userID).ToListAsync();
        }

        public async Task<IEnumerable<Natjecaj>> GetFilteredNatjecajs(NatjecajFilter filter)
        {
            IQueryable<Natjecaj> query = _context.Natjecaji
                .Include(n => n.User)
                .Include(n => n.City).ThenInclude(c => c.County)
                .Include(n => n.Category);

            IQueryable<Natjecaj> query2 = _context.Natjecaji
                .Include(n => n.User)
                .Include(n => n.City).ThenInclude(c => c.County)
                .Include(n => n.Category);

            if (filter.Categories != null)
            {
                query = query.Where(n => filter.Categories.Contains(n.CategoryID));
                query2 = query2.Where(n => filter.Categories.Contains(n.CategoryID));
            }

            if (filter.Counties != null)
            {
                query = query.Where(n => filter.Counties.Contains(n.City.CountyID));
                query2 = query2.Where(n => filter.Counties.Contains(n.City.CountyID));
            }

            if (filter.Cities != null)
            {
                query = query.Where(n => filter.Cities.Contains(n.City.Id));
            }

            if (filter.Emergency)
            {
                query = query.Where(n => n.IsEmergency);
                query2 = query2.Where(n => n.IsEmergency);
            }
            List<Natjecaj> natjecajsWithCities = query.ToList();
            List<Natjecaj> allNatjecajs = query2.ToList();

            if (filter.Cities != null)
            {
                allNatjecajs = allNatjecajs.Where(d => !natjecajsWithCities.Any(p => p.City.CountyID == d.City.CountyID)).ToList();
                return allNatjecajs.Concat(natjecajsWithCities);
            }

            return allNatjecajs;
        }

        public async Task<Natjecaj> GetNatjecajById(Guid id)
        {
            return await _context.Natjecaji.Include(x=>x.City)
                                           .Include(x=>x.City.County)
                                           .Include(x=>x.User)
                                           .Include(x=>x.Category)
                                           .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
