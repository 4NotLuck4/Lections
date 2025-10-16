using Microsoft.Data.SqlClient;
using System.Data;

Console.WriteLine("Разработка клиента");

var connectionString = "...";
using IDbConnection connection = new SqlConnection(connectionString);
string query = "";
connection.Open();

