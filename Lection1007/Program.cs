using Lections1007.Contexts;
using Lections1007.DTOs;
using Lections1007.FIlteres;
using Lections1007.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

Console.WriteLine("Применение ORM [EF Core]");

//using var context = new AppDbContext();
var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
optionsBuilder.UseSqlServer(@"Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True");
optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

using var context = new StoreDbContext(optionsBuilder.Options);

context.ChangeTracker.QueryTrackingBehavior =
    QueryTrackingBehavior.NoTracking;

var games = context.Games
    .Join(context.Categories,
        g => g.CategoryId,
        c => c.CategoryId,
        (g, c) => new
        {
            g.Name,
            Category = c.Name,
        });

var badgames = context.Games
    .Join(context.Categories,
        g => g.GameId,
        c => c.CategoryId,
        (g, c) => new
        {
            g.GameId,
            g.Name,
            Category = c.Name,
        });

Console.WriteLine(badgames.ToQueryString());
Debug.WriteLine(games.ToQueryString());
Trace.WriteLine(games.ToQueryString());

var isAllGamesDeleted = context.Games
    .All(g => g.IsDeleted);

var hasUndeletedGames = context.Games
    .Any(g => !g.IsDeleted);

//var categoryService = new CategoryService(context);
//var categories = await categoryService.GetCategoriesAsync();

//foreach (var category in categories)
//    Console.WriteLine(category.Name);

static async Task GetValueAsync(AppDbContext context)
{
    var btGame = context.Games.FirstOrDefault(x => x.GameId == 3);
    var game = await context.Games.FindAsync(1);
    game = context.Games.Find(1);

    game = context.Games.FirstOrDefault(g => g.GameId > 2);
    game = await context.Games.FirstOrDefaultAsync(g => g.GameId > 2);

    game = context.Games.SingleOrDefault(g => g.GameId == 2); //Должен принимать лишь одно вхождение
    game = await context.Games.SingleOrDefaultAsync(g => g.GameId == 2);
}

static void GetList(AppDbContext context)
{
    var categories = context.Categories.Include(c => c.Games);
    foreach (var category in categories)
        Console.WriteLine($"[{category.CategoryId}] - {category.Name}, {category.Games?.Count()}");

    Console.WriteLine();

    var games = context.Games.Include(g => g.Category);
    foreach (var game1 in games)
        Console.WriteLine($"[{game1.GameId}] - {game1.Name}, {game1.Category?.Name}");
}

static async Task AddCategory(AppDbContext context)
{
    //Insert - добавление значений в таблицу
    var category = new Category()
    {
        Name = "rogue-like"
    };

    //context.Categories.Add(category);
    //context.SaveChanges();

    await context.Categories.AddAsync(category);
    await context.SaveChangesAsync();
}

static async Task RemoveCategoryAsync(AppDbContext context)
{
    //delete - Удаление объекта
    var category = await context.Categories.FindAsync(8);
    if (category is not null)
    {
        context.Categories.Remove(category);

        await context.SaveChangesAsync();
    }
    //AddRange(), RemoveRange()
}

static async Task UpdateCategoryFromDbAsync(AppDbContext context)
{
    //update - Обновление данных
    //1 - получаем объект и меняем его
    var category = await context.Categories.FindAsync(1);

    if (category is null)
        throw new ArgumentException("category isn't found");

    category.Name = "simulator";

    await context.SaveChangesAsync();
}

static async Task UpdateCategoryFromCodeAsync(AppDbContext context)
{
    //update - Обновление данных
    //2 - создаём объект в программе и отправляем изм. данные
    var category = new Category()
    {
        CategoryId = 2,
        Name = "Shooter",
    };

    context.Categories.Update(category);
    await context.SaveChangesAsync();
}

static async Task MassiveUpdateAsync(AppDbContext context)
{
    await context.Games
        .Where(g => g.GameId > 4)
        .ExecuteDeleteAsync();

    await context.Games
        .Where(g => g.CategoryId == 1)
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(g => g.IsDeleted, g => false)
            .SetProperty(g => g.KeysAmount, g => g.KeysAmount > 90 ? 120 : 25));
}

static void Include(StoreDbContext context)
{
    var result = context.Games
        .Include(g => g.Category);
    Console.WriteLine(result.ToQueryString());
    Console.WriteLine();
    foreach (var x in result)
        Console.WriteLine($"{x.Name} - {x.Category?.Name}");


    var categories = context.Categories
        .Include(c => c.Games);
    Console.WriteLine(categories.ToQueryString());
    Console.WriteLine();
    foreach (var x in categories)
        Console.WriteLine($"{x.Name} - {x.Games?.Count()}");
}

static void Pagination(StoreDbContext context)
{
    int pageSize = 3;
    int currentPage = 4;

    var games = context.Games
        .Skip(pageSize * (currentPage - 1))
        .Take(pageSize);

    Console.WriteLine(games.ToQueryString());
    Console.WriteLine();
    foreach (var game in games)
        Console.WriteLine(game.Name);
}

static void Filter(StoreDbContext context)
{
    var filteredGame = context.Games.AsQueryable();

    if (true)
        filteredGame = filteredGame.Where(g => g.Price < 500);
    if (true)
        filteredGame = filteredGame.Where(g => g.Name.Contains("a"));

    Console.WriteLine(filteredGame.ToQueryString());
}

static void FilterClass(StoreDbContext context)
{
    GameFilter filter = new()
    {
        Price = 500,
        Category = "simulator",
    };

    var games = context.Games.AsQueryable();

    if (filter.Price is not null)
        games = games.Where(g => g.Price < filter.Price);
    if (filter.Name is not null)
        games = games.Where(g => g.Name.Contains(filter.Name));
    if (true)
        games = games.Where(g => g.Category.Name.Contains(filter.Category));
}

static void Sort(StoreDbContext context)
{
    var games = context.Games
        .OrderByDescending(g => g.Price);

    games = context.Games
        .OrderBy(g => EF.Property<object>(g, "Name"));

    foreach (var game in games)
        Console.WriteLine($"{game.Name}, {game.Price} руб.");
}

static void Dtos(StoreDbContext context)
{
    var titles = context.Games
        .Select(g => g.Name);

    foreach (var title in titles)
        Console.WriteLine(title);

    var games = context.Games
        .Include(g => g.Category)
        .Select(g => g.ToDto());

    games = context.Games
        .Select(GameExpression.ToDto);

    foreach (var game in games)
        Console.WriteLine($"{game.Title}, {game.Price} - {game.Tax}, [{game.Category}]");
}

static void GroupBy(StoreDbContext context)
{
    var categories1 = context.Games
        .GroupBy(g => g.Category!.Name)
        .Select(group => new
        {
            CategoryName = group.Key,
            GamesCount = group.Count(),
        });

    Console.WriteLine(categories1.ToQueryString());

    Console.WriteLine();

    var categories2 = context.Games
        .GroupBy(g => new { g.Category!.Name, g.IsDeleted })
        .Select(group => new
        {
            CategoryName = group.Key.Name,
            group.Key.IsDeleted,
            GamesCount = group.Count(g => g.IsDeleted),
        });

    Console.WriteLine(categories2.ToQueryString());
}