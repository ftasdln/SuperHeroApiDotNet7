global using SuperHeroApiDotNet7.Models;
global using SuperHeroApiDotNet7.Data;
using SuperHeroApiDotNet7.Services.SuperHeroService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connString = builder.Configuration.GetConnectionString(nameof(DataContext));


var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
    .UseSqlServer(connString)
    .Options;


builder.Services.AddSingleton(dbContextOptions);


builder.Services.AddTransient<ISuperHeroService, SuperHeroService>();


builder.Services.AddDbContext<DataContext>();


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
