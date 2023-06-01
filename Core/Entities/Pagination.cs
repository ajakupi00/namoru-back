namespace Core.Entities
{
    public class Pagination<T> where T : class
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public Pagination()
        {
            Count = 0;
            PageIndex = 1;
            PageSize = 10;
        }
    }
}
