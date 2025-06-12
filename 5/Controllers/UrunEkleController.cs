using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BenimsiteMvc.Controllers
{
    public class UrunEkleController : Controller
    {
        private readonly string csvDosyaYolu = "wwwroot/veriler/urunler.csv";//Dosyanın yazma izni olmalıdır.

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Uruneklevdmodel model)
        {
            if (ModelState.IsValid)
            {
                // wwwroot/veriler klasörü yoksa oluştur
                var klasorYolu = Path.GetDirectoryName(csvDosyaYolu);//Gerçek yolu döndür
                if (!Directory.Exists(klasorYolu))
                {
                    Directory.CreateDirectory(klasorYolu);//Klasör yoksa oluştur
                }

                var yeniSatir = $"{model.Urunadi};{model.Fiyat}";

                // CSV dosyasına yaz
                using (var dosyayaz = new StreamWriter(csvDosyaYolu, append: true, Encoding.UTF8))
                {
                    dosyayaz.WriteLine(yeniSatir);
                }

                TempData["Mesaj"] = "Ürün başarıyla eklendi.";
                return RedirectToAction("Index");//Sayfaya yönlendir
            }

            return View(model); // Veri giriş doğrulama modelini geçemezse form görünümü tekrar gösterilir
        }
    }
}
