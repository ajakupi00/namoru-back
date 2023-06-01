namespace TrazimMestra.Dtos
{
    public class NewNatjecajDto
    {
        public bool IsEmergency { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CountyId { get; set; }
        public Guid CityId { get; set; }
        public string Description { get; set; }
    }
}
