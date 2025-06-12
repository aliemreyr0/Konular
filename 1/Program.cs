using BenimsiteMvc.Data; //VeritabaniContext bu klasörde.
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//appsettings.json dan baðlantý cümlesini al
var baglanticumlesi = builder.Configuration.GetConnectionString("Baglanti1");
//Eðer bir veri eriþim katmanýnda (örneðin DbContext) kullanmak istenirse burada tanýmlanýr

//DbContext servisini ekle (Ef Core için)
builder.Services.AddDbContext<VeritabaniContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Baglanti1"))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Oturum yönetimi ayarlamalarý
//Session MiddleWare için gerekli servis ayarlamalarý
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);//Bekleme süresi 30 dakikadýr. 30 dakika sonra oturum deðiþkenleri silinecek
    options.Cookie.HttpOnly = true; //Çerez sadece sunucu tarafýndan eriþilebilir (Güvenlik amaçlý)
    options.Cookie.IsEssential = true; //Uygulama için çerezin zorunluluðunu belirtir.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();//Oturum yönetimini aktif hale getirelim

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
