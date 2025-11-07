using Lection1106;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("passwords");

AppDbContext context;

(bool flowControl, object value) = LocalUser(out context);


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

static bool IsUserLocked(User user)
{
    if (user.LockedUntil.HasValue && user.LockedUntil <= DateTime.UtcNow)
    {
        user.FailedLoginAttempts = 0;
        user.LockedUntil = null;
        return false;
    }
    return user.LockedUntil.HasValue;
}

static bool IsCorrectPassword(string password, User user)
{
    int attempts = 3;
    int duration = 1;

    if (user.Password != password)
    {
        user.FailedLoginAttempts++;
        if (user.FailedLoginAttempts >= attempts)
            user.LockedUntil = DateTime.UtcNow.AddSeconds(duration);
        return false;
    }
    return true;
}

static void SuccessLogin(User user)
{
    user.FailedLoginAttempts = 0;
    user.LastAccess = DateTime.UtcNow;
}

static (bool flowControl, object value) LocalUser(out AppDbContext context)
{
    var login = "admin";
    var password = "123";
    context = new AppDbContext();
    var user = context.Users.FirstOrDefault(u => u.Login == login);

    if (user is null)
    {
        Console.WriteLine("not found");
        return false;
    }

    if (user.LockedUntil.HasValue && user.LockedUntil >= DateTime.UtcNow)
    {
        Console.WriteLine($"too early. wait {user.LockedUntil}");
        return false;
    }
    if (IsUserLocked(user))
    {
        Console.WriteLine($"locked until {user.LockedUntil:HH:mm:ss}");
        return false;
    }

    if (IsCorrectPassword(password, user))
    {
        Console.WriteLine("incorect  password");
        context.SaveChanges();
        return false;
    }
    SuccessLogin(user);
    context.SaveChanges();

    Console.WriteLine("welcome");
    return;
}