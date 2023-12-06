global using SuperHeroApiDotNet7.Models;
global using SuperHeroApiDotNet7.Data;
using SuperHeroApiDotNet7.Services.SuperHeroService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<DataContext>();
//builder.Services.AddScoped<ISuperHeroService, SuperHeroService>();

// builder nesnesinin içindeki configurationdan connection stringi aldým 
// buraya contextin adýný vermen lazým bu gider appsetting içinden bulur ilgili contextin con stringini
// ayrýca 2 tane var sen development a yazmýþsýn üsttekine yazcan, procta bu çalýþýyor yani öbürünün aktif olmasý için enviroment ayarlarý yapmak gereekiyor. bilmek biþey katmaz
// TrustServerCertificate=true;" bunu yazmadýðým için önce alttaki hatayý verdi, dediðin gibi ssl problemi yaþatýyor express olduðu için bunu ekledim sonuna bu da düzeldi
string? connString = builder.Configuration.GetConnectionString(nameof(DataContext));


// use sql server func burda kullandým yani sen diðerinde onconfiguring i override edip kullanmýþsýn bi farký yok yani ha bura ha ora, ayný görevi yapar ama projene göre deðiþiyor
var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
    .UseSqlServer(connString)
    .Options;


// yukardaki option builderi ayaða kaldýrdýk
builder.Services.AddSingleton(dbContextOptions);


//senin servisin dep. injectioný
builder.Services.AddTransient<ISuperHeroService, SuperHeroService>();


// bu da zaten contextin eklenmesi 

// sen database in ismini deðiþtirirsin ben sql de drop ettim
// tekrar mig basar kullanýrsýn var mý baþka problem kanka yok bamsý allah razý gelsýn :)eyvallah bamsým biþey olursa yaz 
// bu arada ben wrapli kullanýyorum bunu horizantýl scroll gelmesin diye uyuz ediyo o beni bu þekilde kullanýrsan bence senin de yararýna olur wraplý derken got it ! thanks mate .d
//kolay gelsin bamsýma saðol kankam sanada wwweywallahhhh
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
