
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WordsAPI.Core.Repositories;
using WordsAPI.Core.UnitOfWorks;
using WordsAPI.Repository;
using WordsAPI.Repository.Repositories;
using WordsAPI.Repository.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddDbContext<AppDbContext>(z =>
{
    z.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options => {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
