using Microsoft.EntityFrameworkCore;
using Rinha.Data.Redis;
using Rinha.Data.Context;
using Rinha.Data.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DataContext>(
    options => options.UseNpgsql(builder.Configuration["dbContextSettings:ConnectionString"]));

builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();


// builder.Services.AddSingleton<IDatabase>(cfg =>
//      {
//          IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect($"{builder.Configuration["RedisConnection"]}");
//          return multiplexer.GetDatabase();
//      });


builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configurationOptions = ConfigurationOptions.Parse(builder.Configuration["RedisConnection"]);
    // Adjust the connection pool settings as needed
    configurationOptions.ConnectTimeout = 100; // Adjust the timeout value
    configurationOptions.SyncTimeout = 10;    // Adjust the timeout value
    configurationOptions.AbortOnConnectFail = false; // Optionally, handle connection failures gracefully
    configurationOptions.ConnectRetry = 1000;
    return ConnectionMultiplexer.Connect(configurationOptions);
});


builder.Services.AddSingleton<IRedisService, RedisService>();



var app = builder.Build();

app.MapControllers();

app.Run();
