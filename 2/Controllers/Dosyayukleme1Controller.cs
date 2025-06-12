using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BenimsiteMvc.Controllers
{
    public class Dosyayukleme1Controller : Controller
    {
        //Dosya yükleme işlemini yapar
        private const long Maksimumboyut = 2 * 1024 * 1024;//2Mb üzerindeki dosyalar yüklenemeyecek.
        private readonly string[] GecerliUzantilar = { ".jpg",".jpeg",".png"};//Yüklenebilecek uzantılar

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //async, metodun asenkron olduğunu belirtir.Bu, metodun içerisinde uzun sürebilecek işlemlerin yapılacağı ve
        //bu işlemler yapılırken uygulamanın diğer işlemlerinin engellenmeyeceği anlamına gelir.
        //Asenkron işlemler genellikle dosya yükleme, I/O (giriş/çıkış) işlemleri, veri tabanı sorguları veya dosya okuma/yazma işlemleri
        //gibi zaman alıcı işlemleri içerir.
        //Task: Asenkron metodun geri dönüş türüdür ve metodun çalışmasının bitmesini bekler. 
        public async Task<IActionResult> DosyaYukle(DosyaModel model)
        {
            if (!ModelState.IsValid) {
                ViewData["mesaj"] = "Lütfen bir dosya seçiniz";
                return View("Index");//Index metodunun görünüm sayfasını aç           
            }
            //Dosya bilgilerini alalım
            var dosya = model.Dosya;
            var uzanti = Path.GetExtension(dosya.FileName).ToLower();//Dosya uzantısı
            //Geçerli dosya Uzantı kontrolü
            if (!GecerliUzantilar.Contains(uzanti))
            {
                ViewData["mesaj"] = "Sadece .jpg, .jpeg ve .png dosyaları yüklenebilir.";
                return View("Index");
            }//if
            //Boyut kontrolü
            if(dosya.Length > Maksimumboyut)
            {
                ViewData["mesaj"] = "Dosya boyutu en fazla " + (Maksimumboyut / 1024 / 1024) + " Mbyte olmalıdır.";
                return View("Index");
            }//if
            //wwwroot klasöründe "yukleme" klasörü oluşturulmalıdır. Sunucuda iken bu klasöre dosya yazma izni verilmelidir.
            var kayityolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "yukleme", dosya.FileName);
            //Yükleme işlemi
            using (var dosyabilgi = new FileStream(kayityolu, FileMode.Create))
            {
                await dosya.CopyToAsync(dosyabilgi);//Asenkron bir işlemi beklemek için await kullanılır.
            }//using
            ViewData["mesaj"] = "Dosya başarıyla yüklendi";
            return View("Index");
        }
    }
}
