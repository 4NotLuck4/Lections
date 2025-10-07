using Lection1007.Contexts;
using Microsoft.EntityFrameworkCore;
Console.WriteLine("Hello, World!");

using var context = new AppDbContext();

var categories = context.Categories;
foreach (var category in categories)
    Console.WriteLine($"{category.CategoryId} {category.Name} {category.Games?.Count()}");

Console.WriteLine();

var games = context.Games.Include(g => g.GameId > 2);
foreach (var game1 in games)
    Console.WriteLine($"{game1.GameId} {game1.Name} {game1.Price} {game1.Category?.}");

var game = context.Games.Find(1);
game = await context.Games.FindAsync(1);

game = context.Games.FirstOrDefault(g => g.GameId > 2);
game = await context.Games.FirstOrDefaultAsync(g => g.GameId > 2);

game = context.Games.SingleOrDefault(g => g.GameId > 2);
game = await context.Games.SingleOrDefaultAsync(g => g.GameId > 2);
