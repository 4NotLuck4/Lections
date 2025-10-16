namespace Lections1007.DTOs
{
    public class GameDto
    {
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public string? Category { get; set; }
    }
}