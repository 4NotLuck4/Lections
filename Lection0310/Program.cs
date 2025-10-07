using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

Console.WriteLine("Разработка клиента");


/*var connectionString = ""; // или ConnectionStringBuilder
using DbConnection connection = new SqlConnection(connectionString);
string querty = "INSERT INTO Category(title) OUTPUT INSERTED.CategoryId VALUES(@CategoryId)";

connection.МетодDapper(querty, new { парам1 = знач1, парам2 = знач2, ...});
connection.МетодDapper(querty, объект); свойства объекта - параметр

await connection.МетодDapperAsync(...); // асинхронный вызов*/


