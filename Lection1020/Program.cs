// See https://aka.ms/new-console-template for more information
using Lection1020.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

Console.WriteLine("Выполнение SQL-запросов средствами ORM");

using var context = new GamesDbContext();

int price = 1000;
var games = context.Games
    .FromSql($"dbo.GetGamesByPrice {price}");

var id = new SqlParameter("@id", SqlDbType.Int)
{
    Direction = ParameterDirection.Output
};
string category = "arcada2";
context.Database
    .ExecuteSqlRaw($"dbo.AddCategory {category}, {id} OUTPUT", id);
Console.WriteLine(id.Value);

int minPrice = 1000;
int maxPrice = 1000;    
games = context.Games
    .FromSql($"select * from dbo.GetGamesByPrice({minPrice},{maxPrice})");

//var games = context.Games
//    .Where(g => EF.Functions.Like(g.Name, "[a-d]%"));

//decimal addingPrice = -0.5m;
//int changedRows = context.Database
//    .ExecuteSql($"update game set price +={addingPrice}");

//SqlQuery(context);


//var titles = context.Database
//    .SqlQuery<string>($"select * from game");
//Console.WriteLine(titles.ToQueryString());
//Console.WriteLine();

//var minPrice = context.Database
//    .SqlQuery<decimal>($"select min(price) as value from game")
//    .FirstOrDefault();
//Console.WriteLine();


//await FromSql(context);

Console.WriteLine();

static async Task FromSql(GamesDbContext context)
{ 
    var games = context.Games
        .FromSql($"select * from game");
    Console.WriteLine(games.ToQueryString());

    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();
    int price = 1000;
    games = context.Games
        .FromSql($"select * from game where price < {price}");
    Console.WriteLine(games.ToQueryString());

    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    string columnName = "price";
    games = context.Games
       .FromSqlRaw($"select * from game order by {columnName}");
    Console.WriteLine(games.ToQueryString());

    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));


    string title = "SimCity";
    games = context.Games
        .FromSqlRaw($"select * from game where name = '{title}'");
    Console.WriteLine(games.ToQueryString());

    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));


    title = "SimCity";
    games = context.Games
        .FromSqlRaw($"select * from game where name = @p0", title);
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    var sqlTitle = new SqlParameter("@title", "SimCity");
    games = context.Games
        .FromSqlRaw($"select * from game where name = @title", sqlTitle);
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));
}