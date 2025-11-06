using BCrypt.Net;
using Lection1106;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("passwords");


var login = "admin";
var password = "123";
using var context = new AppDbContext();
var user = context.Users.FirstOrDefault(u => u.Login == login);

if (user is null)
{
    Console.WriteLine("not found");
    return;
}

if (user.LockedUntil.HasValue && user.LockedUntil >= DateTime.UtcNow)
{
    Console.WriteLine($"too early. wait {user.LockedUntil}");
    return;
}
if (user.Password != password)
{
    user.FailedLoginAttempts++;
    if (user.FailedLoginAttempts >= 3)
        user.LockedUntil = DateTime.UtcNow.AddMinutes(1);

    Console.WriteLine($"incorrect");
    return;
}
user.LastAccess = DateTime.UtcNow;
user.FailedLoginAttempts = 0;
user.LockedUntil = null;
context.SaveChanges();

Console.WriteLine("welcome");

static void ComputeHash()
{
    var solt = "123456";
    var password = "qwerty" + solt;
    byte[] bytes = Encoding.UTF8.GetBytes(password);

    SHA384 algo = SHA384.Create();

    var hashBytes = algo.ComputeHash(bytes);
    var hash = Convert.ToBase64String(hashBytes); //base64
    hash = Convert.ToHexString(hashBytes); // hex: 0-9A-F
}

static void ComputeBcryptHash()
{
    var password = "qwerty";
    var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    Console.WriteLine(hash);

    var input = "qwerty";
    var isCorrect = BCrypt.Net.BCrypt.EnhancedVerify(input, hash);
    Console.WriteLine(isCorrect);
}

static async Task InsertData()
{
    var users = new List<User>()
{
    new() {Login = "admin", Password = "qwerty"},
    new() {Login = "manager", Password = "123"},
    new() {Login = "customer", Password = "1"},
};
    var context = new AppDbContext();
    context.Users.AddRange(users);
    await context.SaveChangesAsync();
}