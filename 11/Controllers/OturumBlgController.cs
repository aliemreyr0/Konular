using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class OturumBlgController : Controller
    {
        /*
        Program.cs dosyası içerisine aşağıdaki satırlar eklenmelidir.
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);//Bekleme süresi 30 dakikadır
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        App metotlarının olduğu alana UseSession() yazılır.
        app.UseSession();
         
         */
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OturumOlustur(string anahtar, string deger)
        {
            //Oturum değişkeni oluşturuluyor
            HttpContext.Session.SetString(anahtar, deger);//Anahtar: Kullanici, Değer: ali
            ViewData["mesaj"] = $"{anahtar} anahtarı ile oturum değişkeni oluşturuldu.";
            return View("Index");
        }
        //Oturum verilerini okuma
        [HttpGet]
        public IActionResult OturumOku(string anahtar)
        {
            //Oturum verisini okur
            var deger = HttpContext.Session.GetString(anahtar);
            ViewData["OturumDeger1"] = deger ?? "Belirtilen anahtar ile oturum verisi bulunamadı";
            return View("Index");
        }

        //Oturum verilerini silme
        [HttpPost]
        public IActionResult OturumTemizle()
        {
            //Tüm oturum değişkenlerini temizle
            HttpContext.Session.Clear();//Tek tek temizlemek için .Remove(anahtar) metodu kullanılır.
            ViewData["mesaj"] = "Tüm oturum değişkenleri temizlendi";
            return View("Index");
        }
    }
}
