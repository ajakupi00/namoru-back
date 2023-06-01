using Core.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.SeedData
{
    public class SeedDb
    {
        public static async Task SeedAsync(ApplicationContext context)
        {
            List<City> cities = new List<City>();
            List<County> counties = new List<County>();
            List<Category> categories = new List<Category>();

            User user = new User();
            Mestar ivan = new Mestar();
            if (!context.Countries.Any())
            {
                var countryData = File.ReadAllText("../Infrastructure/Data/SeedData/Country.json");
                var countries = JsonSerializer.Deserialize<Country>(countryData);
                context.Countries.AddRange(countries);
            }
            if (!context.Counties.Any() && !context.Cities.Any())
            {
                var croatiaData = File.ReadAllText("../Infrastructure/Data/SeedData/hr.json");
                var data = JsonSerializer.Deserialize<List<CroatianData>>(croatiaData);

                foreach (CroatianData result in data)
                {
                    if(!counties.Any(c => c.Name == result.admin_name))
                    {
                    counties.Add(new County() { Id = Guid.NewGuid(), Name = result.admin_name });
                    }
                }
                foreach (CroatianData result in data)
                {
                    County county = counties.FirstOrDefault(c => c.Name == result.admin_name);
                    cities.Add(new City()
                    {
                        Id = Guid.NewGuid(),
                        Name = result.city,
                        CountyID = county.Id,
                        //CROATIA ONLY, EDIT WHEN ADDING ANOTHER COUNTRY
                        CountryID = Guid.Parse("0d01933e-8e90-46b7-a058-6c2d7f8c9de9")
                    });
                }
                context.Counties.AddRange(counties);
                context.Cities.AddRange(cities);
            }
            if (!context.Categories.Any())
            {
                var categoryData = File.ReadAllText("../Infrastructure/Data/SeedData/Category.json");
                categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
                context.Categories.AddRange(categories);
            }
            if (!context.Users.Any())
            {
                user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Lepi",
                    LastName = "Miške",
                    Email = "lepi@gmail.com",
                    Password = "password",
                    CityID = cities.FirstOrDefault(g => g.Name == "Zadar").Id,
                    IsMestar = false
                };
                context.Users.AddRange(user);
            }
            if (!context.Mestri.Any())
            {

                ivan = new Mestar() 
                { 
                    Id= Guid.Parse("a3f63892-1425-403c-9e73-1059e6113826"),
                    FirstName = "Ivan",
                    LastName = "Horvat",
                    Email = "ivan@gmail.com",
                    Password = "password",
                    CityID = cities.FirstOrDefault(g => g.Name == "Zagreb").Id,
                };

                context.Mestri.AddRange(ivan);
            }
            if (!context.Natjecaji.Any())
            {
                List<Natjecaj> natjecaji = new List<Natjecaj>();
                Natjecaj natjecaj = new Natjecaj()
                {
                    Id = Guid.NewGuid(),
                    UserID = user.Id,
                    CityID = cities.FirstOrDefault(g => g.Name == "Zagreb").Id,
                    CategoryID = categories.FirstOrDefault(c => c.Name == "Mehaničar").Id,
                    IsEmergency = false,
                    Description = "Samo problemi tebra",
                    Created = DateTime.UtcNow,
                };
                Natjecaj natjecaj2 = new Natjecaj()
                {
                    Id = Guid.NewGuid(),
                    UserID = user.Id,
                    CityID = cities.FirstOrDefault(g => g.Name == "Zadar").Id,
                    CategoryID = categories.FirstOrDefault(c => c.Name == "Električar").Id,
                    IsEmergency = true,
                    Description = "Samo problemi brate",
                    Created = DateTime.UtcNow,
                };
                Natjecaj natjecaj3 = new Natjecaj()
                {
                    Id = Guid.NewGuid(),
                    UserID = user.Id,
                    CityID = cities.FirstOrDefault(g => g.Name == "Zadar").Id,
                    CategoryID = categories.FirstOrDefault(c => c.Name == "Električar").Id,
                    IsEmergency = true,
                    Description = "Samo problemi brate",
                    Created = DateTime.UtcNow,
                };

                natjecaji.Add(natjecaj);
                natjecaji.Add(natjecaj2);
                natjecaji.Add(natjecaj3);

                context.Natjecaji.AddRange(natjecaji);
            }

            if (!context.Reviews.Any())
            {
                var reviewIvana = new Review
                {
                    Id = Guid.NewGuid(),
                    Mestar = ivan,
                    MestarId = ivan.Id,
                    Reviewer = user,
                    ReviewerId = user.Id,
                    City = cities.FirstOrDefault(g => g.Name == "Zagreb"),
                    CityId = cities.FirstOrDefault(g => g.Name == "Zagreb").Id,
                    Rating = (decimal)4.6,
                    Description = "Odličan posao!"
                };

                context.Reviews.AddRange(reviewIvana);
            }

            if (context.ChangeTracker.HasChanges()) 
                await context.SaveChangesAsync();
        }
    }
}
