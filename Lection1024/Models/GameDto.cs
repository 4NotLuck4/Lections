namespace Lection1024.Models
{
    public class GameDto
    {
        public int GameId { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
    }

    public static class GameExtensions
    {
        public static GameDto ToDto(this Game g)
            => new()
            {
                GameId = g.GameId,
                Name = g.Name,
                Price = g.Price,
                Category = g.Category.Name
            };
    }
}
