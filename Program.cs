using Microsoft.EntityFrameworkCore;
using Rinha.Data.Redis;
using Rinha.Data.Context;
using Rinha.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DataContext>(
    options => options.UseNpgsql(builder.Configuration["dbContextSettings:ConnectionString"]));

builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
