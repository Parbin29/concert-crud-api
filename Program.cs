using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hellow World!");

int numberToGuess = 87;
app.MapGet("/guess/{input:int}", (int input) =>
{
    if (input < numberToGuess)
    {
        return "Too low!";
    }
    else if (input > numberToGuess)
    {
        return "Too high!";
    }
    else
    {
        return "Correct number!";
    }
});

app.MapGet("/time", () => DateTime.Now.ToString("F"));

//adding a concert
app.MapPost("/create-concert", async (HttpRequest req) => {
    var formData = await req.ReadFormAsync();
    var name = formData["name"];
    var venue = formData["venue"];
    var date = formData["date"];
    var capacity = formData["capacity"];

    var concertData = new {
        Name = name,
        Venue = venue,
        Date = date,
        Capacity = capacity
    };

    Console.WriteLine($"Added concert with name: {concertData.Name}, Venue: {concertData.Venue} Date: {concertData.Date}, Capacity: {concertData.Capacity}");
    return Results.Created("/create-concert", $"Added concert with name: {concertData.Name}, Venue: {concertData.Venue} Date: {concertData.Date}, Capacity: {concertData.Capacity}");
                   
});


app.Run();


