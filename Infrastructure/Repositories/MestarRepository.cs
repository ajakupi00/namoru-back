using Core.Entities;
using Core.interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Repositories
{
    public class MestarRepository : IMestarRepository
    {
        private readonly ApplicationContext _context;
        public MestarRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mestar>> Search(MestarFilter search)
        {
            var mestri = _context.Mestri
                            .Include(m => m.MestarCategories)
                            .Include(m => m.MestarCategories).ThenInclude(mc => mc.Category)
                            .Include(m => m.City).ThenInclude(c => c.County)
                            .AsQueryable();

            if (search.Categories != null && search.Categories.Any())
            {
                var mestarCategoriesForSelectedCategories = _context.MestarCategories
                    .Where(mc => search.Categories.Contains(mc.CategoryId))
                    .Include(mc => mc.Mestar)
                    .Include(mc => mc.Category)
                    .ToList();

                mestri = mestri.Where(m => m.MestarCategories.Any(mc => mestarCategoriesForSelectedCategories.Select(mcs => mcs.MestarId).Contains(mc.MestarId))).AsQueryable();

            }

            if (search.Cities != null && search.Cities.Any())
                mestri = mestri.Where(m => search.Cities.Contains(m.CityID));

            if (search.Counties != null && search.Counties.Any())
                mestri = mestri.Where(m => search.Counties.Contains(m.City.CountyID));

            if (!string.IsNullOrWhiteSpace(search.Name))
                mestri = mestri.Where(m => m.FirstName.ToLower().Contains(search.Name.ToLower()) ||
                                      m.LastName.ToLower().Contains(search.Name.ToLower()));

            calculateReviews(mestri);

            mestri = mestri.Skip((search.CurrentPage - 1) * search.PageSize)
                           .Take(search.PageSize);

            return mestri.ToList();
        }

        private void calculateReviews(IQueryable<Mestar> mestri)
        {
            mestri.ToList().ForEach(mestar =>
            {
                var reviews = _context.Reviews.Where(r => r.MestarId == mestar.Id);
                decimal rating;
                if (!reviews.Any())
                    rating = 0;
                else
                    rating = reviews.Select(r => r.Rating).Average();
                mestar.Rating = rating;
                mestar.Reviews = reviews.Count();
            });
        }
    }
}
