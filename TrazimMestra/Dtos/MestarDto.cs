using Core.Entities;

namespace TrazimMestra.Dtos
{
    public class MestarDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public City City { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
        public decimal Rating { get; set; }
        public int Reviews { get; set; }
        public string Description { get; set; }


    }
}
