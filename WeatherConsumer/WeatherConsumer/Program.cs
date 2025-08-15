using Microsoft.EntityFrameworkCore;
using WeatherApi.Extensions;
using WeatherConsumer.Models.Configs;
using WeatherConsumer.Services;
using WeatherConsumer.Util;
using Microsoft.EntityFrameworkCore.Design;
using WeatherConsumer.Interfaces.Repositories;
using WeatherConsumer.Repositories;
using WeatherConsumer.Interfaces.Services;
using WeatherConsumer.Factorys;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRabbit(builder.Configuration);
builder.Services.AddHostedService<WeatherConsumerService>();
builder.Services.AddTransient<IWeatherReapository, WeatherReapository>();
builder.Services.AddTransient<IScopedFactory, ScopedFactory>();

string connectionString = builder.Configuration.GetSection("SqlConnection:ConnectionString").Value;

builder.Services.AddDbContext<SqlDataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
