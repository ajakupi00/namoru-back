namespace Core.Entities
{
    public class Review : BaseEntity
    {
        public Guid MestarId{ get; set; }
        public Mestar Mestar{ get; set; }
        public Guid ReviewerId { get; set; }
        public User Reviewer { get; set; }
        public Guid CityId { get; set; }
        public City City{ get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }

    }
}
