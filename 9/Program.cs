using Benimsite2.Servisler;

var builder = WebApplication.CreateBuilder(args);

// Servisi baðýmlýlýk enjeksiyonuna ekle
builder.Services.AddScoped<IServis1, Servis1>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
