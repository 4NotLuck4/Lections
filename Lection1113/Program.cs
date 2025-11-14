using Lection1113;

Console.WriteLine("jwt");

// htpps://www.jwt.io/

AuthService service = new();
var accessToken = service.GenerateToken(123, "user1");
Console.WriteLine(accessToken);

if (service.IsValidToken(accessToken))
    Console.WriteLine("ock");
else
    Console.WriteLine("error");