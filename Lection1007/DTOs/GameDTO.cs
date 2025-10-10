using Lection1007.Models;

namespace Lection1007.DTOs
{
    internal class GameDto
    {
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public string? Category { get; set; }
    }
    public static class GameExtension
    {
        public static GameDto? ToDto(this Game game)
        {
            if (game == null) 
                return null;
            return new GameDto
            {
                Title = game.Name,
                Price = game.Price,
                Tax = game.Price * 0.2m,
                Category = game.Category.Name ?? ""
            };
        }

        public static IEnumerable<GameDto> ToDtos(this IEnumerable<Game> games)
            => games.ToDtos();

    }


    public static class GameExpression
    {
        public static Exception<Func<Game, GameDto>> ToDto
            => game => new GameDto
            {
                Title = game.Name,
                Price = game.Price,
                Tax = game.Price * 0.2m,
                Category = game.Category.Name ?? ""
            };
    }
}
