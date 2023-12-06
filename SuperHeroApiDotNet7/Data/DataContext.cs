global using Microsoft.EntityFrameworkCore;

namespace SuperHeroApiDotNet7.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {//buraya db context yazmışsın data context yerine sana tavsiye veriyim buraya böyle genel bi isim yazma böyle karışıyor hep, SuperHeroContext yazabilirsin, dataContext diyiince birbirine giriyor sanki önemli bişey gibi geliyor halbuki sikik bir context :)
                  
                //dur bir diğeri burdaki onconfiguring burda ayar yapacaksan bi işe yarar, 
                // sen nerde yapıyon bilmiyom da bu küçük projede burayı karıştırmaya gerek yok bak
        }

        // A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - Sertifika zinciri güvenilmeyen bir yetkili tarafından verildi.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
// kanka hatan şuydu gördün mü bilmiyorum
//
//optionsBuilder.UseSqlServer();
        }

        public DbSet<SuperHero> superHeroes { get; set; }
    }
}
