using Lections1007.Model;

namespace Lections1007.DTOs
{
    public static class GameExtension
    {
        public static GameDto? ToDto(this Game game)
            => game is null ? null : new GameDto
            {
                Title = game.Name,
                Price = game.Price,
                Tax = game.Price * 0.2m,
                Category = game.Category?.Name
            };

        public static IEnumerable<GameDto> ToDtos(this IEnumerable<Game> games)
                => games.Select(g => g.ToDto());
    }
}