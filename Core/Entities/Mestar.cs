using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Mestar : User
    {
        public Mestar()
        {
            IsMestar = true;
        }

        [NotMapped]
        public decimal Rating { get; set; }
        [NotMapped]
        public int Reviews { get; set; }
        public ICollection<MestarCategory> MestarCategories { get; set; }
    }
}
