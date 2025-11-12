using Microsoft.EntityFrameworkCore;
using MovieRecommender;
using MovieRecommender.Models;
using MovieRecommender.Repositories;
using MovieRecommender.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<JwtConfiguration>(
    builder.Configuration.GetSection("JwtConfiguration"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();