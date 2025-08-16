using Microsoft.EntityFrameworkCore;
using WeatherConsumer.Services;
using WeatherConsumer.Interfaces.Repositories;
using WeatherConsumer.Repositories;
using Util.DbContextService;
using Util.Factorys;
using Util.Interfaces;
using Util.Extensions;

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
