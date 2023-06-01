using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrazimMestra.Attributes;

namespace TrazimMestra.Controllers
{
    public class DataController : BaseApiController
    {
        private readonly IGenericRepository<City> _cityRepo;
        private readonly IGenericRepository<County> _countyRepo;
        private readonly IGenericRepository<Category> _categoryRepo;

        public DataController(IGenericRepository<City> cityRepo, IGenericRepository<County> countyRepo, IGenericRepository<Category> categoryRepo)
        { 
            _cityRepo = cityRepo;
            _countyRepo = countyRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet("cities")]
        public async Task<ActionResult<City>> GetCities(Guid countyId)
        {
            var city = await _cityRepo.ListAllAsync();     
            var filtered = city.Where(c => c.CountyID == countyId).ToList();
            if (city == null)
            {
                return NotFound();
            }

            return Ok(filtered);
        }

        [HttpGet("counties")]
        public async Task<ActionResult<County>> GetCounties()
        {
            var counties = await _countyRepo.ListAllAsync();

            if (counties == null)
            {
                return NotFound();
            }

            return Ok(counties);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<County>> GetCategories()
        {
            var categories = await _categoryRepo.ListAllAsync();

            if (categories == null)
            {
                return Ok();
            }

            return Ok(categories);
        }

    }
}
