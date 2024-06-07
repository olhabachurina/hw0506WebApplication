
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot/index.html");
});

app.MapGet("/weather", async (context) =>
{
    var cityName = context.Request.Query["city"];
    var apiKey = "625d6af406c13e75f9a88219ca07468b";
    var url = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric&lang=en";

    using (var client = new HttpClient())
    {
        var response = await client.GetStringAsync(url);
        var json = JObject.Parse(response);

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(json.ToString());
    }
});

app.Run();