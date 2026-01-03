using DataLayer;
using DataLayer.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

// App Start
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
services.AddDbContext(configuration);
services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Migrate database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();