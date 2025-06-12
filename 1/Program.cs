using BenimsiteMvc.Data; //VeritabaniContext bu klas�rde.
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//appsettings.json dan ba�lant� c�mlesini al
var baglanticumlesi = builder.Configuration.GetConnectionString("Baglanti1");
//E�er bir veri eri�im katman�nda (�rne�in DbContext) kullanmak istenirse burada tan�mlan�r

//DbContext servisini ekle (Ef Core i�in)
builder.Services.AddDbContext<VeritabaniContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Baglanti1"))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Oturum y�netimi ayarlamalar�
//Session MiddleWare i�in gerekli servis ayarlamalar�
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);//Bekleme s�resi 30 dakikad�r. 30 dakika sonra oturum de�i�kenleri silinecek
    options.Cookie.HttpOnly = true; //�erez sadece sunucu taraf�ndan eri�ilebilir (G�venlik ama�l�)
    options.Cookie.IsEssential = true; //Uygulama i�in �erezin zorunlulu�unu belirtir.
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
app.UseSession();//Oturum y�netimini aktif hale getirelim

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
