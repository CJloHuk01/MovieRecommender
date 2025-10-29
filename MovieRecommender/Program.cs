using Microsoft.EntityFrameworkCore;
using MovieRecommender.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();