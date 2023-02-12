using Microsoft.AspNetCore.Mvc.Versioning;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add Logging Modules
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPlayerService, PlayerService>();
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<IStatisticService, StatisticsService>();
builder.Services.AddSingleton<IStatisticRepository, StatisticsRepository>();

builder.Services.AddApiVersioning(o =>
{
    o.ApiVersionReader = new HeaderApiVersionReader("api-version");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
