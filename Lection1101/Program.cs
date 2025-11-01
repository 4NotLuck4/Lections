using Lection1101.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

Console.WriteLine("call web-api");

//↓ настройка http клиента ↓
var client = new HttpClient();
string baseUrl = "https://api.escuelajs.co/api/v1/";
client.BaseAddress = new Uri(baseUrl);



var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // с маленькой буквы названия
    WriteIndented = true,       // для отступов
};


var category = new Category { Name = "testr36h62q4r", Image = "https://placeimg.com/640/480/any" };
var json = JsonSerializer.Serialize(category, jsonOptions);
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync("cstegories", content);
response.EnsureSuccessStatusCode();

// получение объекта из ответа
if (response.IsSuccessStatusCode)
{
    var responseJson = await response.Content.ReadAsStringAsync();
    category = JsonSerializer.Deserialize<Category>(responseJson, jsonOptions);
}

Console.WriteLine();

//var json = JsonSerializer.Serialize(объект, jsonOptions);
//var content = new StringContent(json, Encoding.UTF8, "application/json");

//var response = await _client.МетодAsync(…, content);
//response.EnsureSuccessStatusCode();

//// получение объекта из ответа
//if (response.IsSuccessStatusCode)
//    var responseJson = await response.Content.ReadAsStringAsync();



static async Task<(HttpResponseMessage response, HttpResponseMessage response2, HttpResponseMessage response3)> TestApi(HttpClient client)
{
    var categories = await client.GetFromJsonAsync<List<Category>>("categories");

    int id = 41;
    var category = await client.GetFromJsonAsync<Category>($"categories/{id}");

    category = new Category { Name = "test12321412r362q4r", Image = "https://placeimg.com/640/480/any" };
    using var response = await client.PostAsJsonAsync("categories", category);
    response.EnsureSuccessStatusCode();

    var result = await response.Content.ReadFromJsonAsync<Category>();

    category.Name = "newName";

    using var response2 = await client.PutAsJsonAsync($"categories/{category.Id}", category);

    response2.EnsureSuccessStatusCode();


    using var response3 = await client.DeleteAsync($"categories/{category.Id}");
    response3.EnsureSuccessStatusCode();
    return (response, response2, response3);
}

static async Task<HttpResponseMessage> TestGet(HttpClient client)
{
    // ↓ Пример ↓
    var response = await client.GetAsync("categories");
    response.EnsureSuccessStatusCode();

    var jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // с маленькой буквы названия
        WriteIndented = true,       // для отступов
    };

    var content = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<List<Category>>(content, jsonOptions);
    // ↑
    return response;
}