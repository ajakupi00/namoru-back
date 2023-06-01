namespace Core.Models
{
    public class MestarFilter
    {
        public IEnumerable<Guid>? Cities{ get; set; }
        public IEnumerable<Guid>? Counties{ get; set; }
        public IEnumerable<Guid>? Categories { get; set; }
        public string? Name { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
