using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Move this line before app is built
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<FirebaseService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
      "v1",
      new OpenApiInfo
      {
          Title = "Todo API",
          Description = "Keep track of your tasks",
          Version = "v1"
      }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    });
}

app.MapGet("/", async (FirebaseService firebaseService) =>
{
    try
    {
        await firebaseService.AddDocument("test", "lets go");
        return Results.Ok("Dummy data inserted successfully!");
    }
    catch (Exception err)
    {
        Console.WriteLine("", err.Message);
        return Results.BadRequest("The request failed");
    }
});


app.Run();
