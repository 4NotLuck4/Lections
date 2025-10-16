namespace Lections1007.FIlteres
{
    public class GameFilter
    {
        public string? Name { get; set; }
        public string? Category { get; set; } = "arcada";
        public decimal? Price { get; set; }
    }

    public class Paginator
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
