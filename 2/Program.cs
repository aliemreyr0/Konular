var builder = WebApplication.CreateBuilder(args);
//appsettings.json dan ba�lant� c�mlesini al
var baglanticumlesi = builder.Configuration.GetConnectionString("Baglanti1");
//E�er bir veri eri�im katman�nda (�rne�in DbContext) kullanmak istenirse burada tan�mlan�r

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
