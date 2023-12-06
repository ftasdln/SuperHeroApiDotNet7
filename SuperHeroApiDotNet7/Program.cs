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

// builder nesnesinin i�indeki configurationdan connection stringi ald�m 
// buraya contextin ad�n� vermen laz�m bu gider appsetting i�inden bulur ilgili contextin con stringini
// ayr�ca 2 tane var sen development a yazm��s�n �sttekine yazcan, procta bu �al���yor yani �b�r�n�n aktif olmas� i�in enviroment ayarlar� yapmak gereekiyor. bilmek bi�ey katmaz
// TrustServerCertificate=true;" bunu yazmad���m i�in �nce alttaki hatay� verdi, dedi�in gibi ssl problemi ya�at�yor express oldu�u i�in bunu ekledim sonuna bu da d�zeldi
string? connString = builder.Configuration.GetConnectionString(nameof(DataContext));


// use sql server func burda kulland�m yani sen di�erinde onconfiguring i override edip kullanm��s�n bi fark� yok yani ha bura ha ora, ayn� g�revi yapar ama projene g�re de�i�iyor
var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
    .UseSqlServer(connString)
    .Options;


// yukardaki option builderi aya�a kald�rd�k
builder.Services.AddSingleton(dbContextOptions);


//senin servisin dep. injection�
builder.Services.AddTransient<ISuperHeroService, SuperHeroService>();


// bu da zaten contextin eklenmesi 

// sen database in ismini de�i�tirirsin ben sql de drop ettim
// tekrar mig basar kullan�rs�n var m� ba�ka problem kanka yok bams� allah raz� gels�n :)eyvallah bams�m bi�ey olursa yaz 
// bu arada ben wrapli kullan�yorum bunu horizant�l scroll gelmesin diye uyuz ediyo o beni bu �ekilde kullan�rsan bence senin de yarar�na olur wrapl� derken got it ! thanks mate .d
//kolay gelsin bams�ma sa�ol kankam sanada wwweywallahhhh
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
