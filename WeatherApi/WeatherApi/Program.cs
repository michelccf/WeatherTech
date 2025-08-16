using Microsoft.EntityFrameworkCore;
using Util.DbContextService;
using Util.Extensions;
using WeatherApi.Interfaces;
using WeatherApi.Repositories;
using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPolly();
builder.Services.AddRabbit(builder.Configuration);
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddSingleton<IMessageBroker, MessageBroker>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();

string connectionString = builder.Configuration.GetSection("SqlConnection:ConnectionString").Value;

builder.Services.AddDbContext<SqlDataContext>(options => options.UseSqlServer(connectionString));

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
