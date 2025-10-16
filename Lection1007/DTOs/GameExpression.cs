using Lections1007.Model;
using System.Linq.Expressions;

namespace Lections1007.DTOs
{
    public static class GameExpression
    {
        public static Expression<Func<Game, GameDto>> ToDto
            => game => new GameDto
            {
                Title = game.Name,
                Price = game.Price,
                Tax = game.Price * 0.2m,
                Category = game.Category.Name
            };
    }
}