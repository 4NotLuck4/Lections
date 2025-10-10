using Lection1007.Contexts;
using Lection1007.DTOs;
using Lection1007.Filters;
using Lection1007.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Channels;
Console.WriteLine("именение ORM");

var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3102;Persist Security Info=True;User ID=ispp3102;Password=3102;Encrypt=True;Trust Server Certificate=True");
using var context = new StoreDbContext(optionsBuilder.Options);

var titles = context.Games
    .Select(g => g.Name);

foreach (var t in titles)
    Console.WriteLine(t);
Console.WriteLine();

var games = context.Games
    .Include(g => g.Category)
    .Select(g => g.ToDto());

var testGames = context.Games.ToList();
var dtos = 

foreach (var g in games)
    Console.WriteLine($"{g.Title} {g.Price} {g.Tax} {g.Category}");

Console.WriteLine(games.ToQueryString());

Sort(context);

FilterBy(context);

Filter(context);

//if (true) // заменить условие
//    games = games.Where(g => g.Price < 500);
//if (true) // заменить условие
//    games = games.Where(g => g.Name.Contains("a"));

//Console.WriteLine(games.ToQueryString());

//int pageSize = 3;
//int currentPage = 4;
//var games = context.Games
//    .Skip(pageSize * (currentPage - 1))
//    .Take(pageSize);

//foreach (var game in games)
//    Console.WriteLine(game.Name);

//Console.WriteLine(games.ToQueryString());
//Console.WriteLine();



//Include(context);



//var categoryService = new CategoryServise(context);
//var category = await categoryService3.GetCategoryAsync();
//foreach (var category in categories)
//Console.WriteLine();



//using var context = new AppDbContext();
//GetList(context);

//await AddCategory(context);
//await RemoveCategory(context);

//await UpdateCategoryFromDb(context);
//UpdateCategory(context);

//context.Games
//    .Where(g => g.CategoryId > 4)
//    .ExecuteUpdate(setters => setters
//        .SetProperty(g => g.IsDeleted, g => false)
//        .SetProperty(g => g.Price, g => g.Price > 2000 ? 3000 : 1000)
//        .SetProperty(g => g.KeysAmount, g => g.KeysAmount > 2000 ? 3000 : 1000));

static async Task AddCategory(AppDbContext context)
{
    //insert
    var category = new Category()
    {
        Name = "casual"
    };
    context.Categories.Add(category);
    context.SaveChanges();

    await context.Categories.AddAsync(category);
    await context.SaveChangesAsync();
}

static async Task RemoveCategory(AppDbContext context)
{
    //delete
    var category = context.Categories.Find(6);
    if (category is not null)
    {
        context.Categories.Remove(category);

        context.SaveChanges();
        await context.SaveChangesAsync();
    }
}

static async Task UpdateCategoryFromDb(AppDbContext context)
{
    // update
    // 1
    var category = context.Categories.Find(1);
    if (category is null)
        throw new ArgumentException("егория не найдена");
    category.Name = "arcada";
    context.SaveChanges();
    await context.SaveChangesAsync();
}

static void UpdateCategory(AppDbContext context)
{
    // update2
    var category = new Category()
    {
        CategoryId = 1,
        Name = "аркада"
    };
    context.Categories.Update(category);
    context.SaveChanges();
}

static void GetList(AppDbContext context)
{
    var categories = context.Categories;
    foreach (var category in categories)
        Console.WriteLine($"{category.CategoryId} {category.Name} {category.Games?.Count()}");

    Console.WriteLine();

    var games = context.Games.Include(g => g.GameId > 2);
    foreach (var game1 in games)
        Console.WriteLine($"{game1.GameId} {game1.Name} {game1.Price} {game1.Category}");
}

static void Include(StoreDbContext context)
{
    var result = context.Games
        .Include(g => g.Category);
    Console.WriteLine(result.ToQueryString());
    foreach (var x in result)
        Console.WriteLine($"{x.Name} {x.Category?.Name}");
    Console.WriteLine();

    var categories = context.Categories
        .Include(g => g.Games);
    foreach (var x in categories)
        Console.WriteLine($"{x.Name} {x.Games?.Count()}");
    Console.WriteLine(result.ToQueryString());
    Console.WriteLine();
}

static void Filter(StoreDbContext context)
{
    var games = context.Games.AsQueryable();

    Console.WriteLine("Фильтрация по цене?");
    var answer = Console.ReadLine();

    if (answer == "yes") // заменить условие
        games = games.Where(g => g.Price < 500);
    if (true) // заменить условие
        games = games.Where(g => g.Name.Contains("a"));
    if (true) // заменить условие
        games = games.Where(g => !String.IsNullOrWhiteSpace(g.Description));
    if (true) // заменить условие
        games = games.Where(g => g.Category.Name == "arcada");
    Console.WriteLine(games.ToQueryString());
}

static void FilterBy(StoreDbContext context)
{
    GameFilter filter = new()
    {
        Price = 500,
        Category = "аркада"
    };

    var games = context.Games.AsQueryable();
    if (filter.Price is not null) // заменить условие
        games = games.Where(g => g.Price < filter.Price);
    if (filter.Name is not null) // заменить условие
        games = games.Where(g => g.Name == filter.Name);
    if (filter.Category is not null) // заменить условие
        games = games.Where(g => g.Category.Name == filter.Category);
}

static void Sort(StoreDbContext context)
{
    var games = context.Games
        .OrderByDescending(g => g.Price);

    games = context.Games
        .OrderByDescending(g => EF.Property<object>(g, "Name"));

    foreach (var g in games)
        Console.WriteLine($"{g.Name} {g.Price}");
}

//var game = context.Games.Find(1);
//game = await context.Games.FindAsync(1);

//game = context.Games.FirstOrDefault(g => g.GameId > 2);
//game = await context.Games.FirstOrDefaultAsync(g => g.GameId > 2);

//game = context.Games.SingleOrDefault(g => g.GameId > 2);
//game = await context.Games.SingleOrDefaultAsync(g => g.GameId > 2);

