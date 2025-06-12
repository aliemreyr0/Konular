using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class SayfatipleriController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public RedirectResult Sayfayagit()
        {
            //Sayfaya yönlendirme yapar.
            return Redirect("~/Home/Privacy");
        }

        public ContentResult Iceriksun()
        {
            //Bir içerik (Metin veya Html) sunmayı sağlar
            return Content("<h2>Merhaba Ziyaretçi.</h2>", "text/html", System.Text.Encoding.UTF8);
        }

        public JsonResult Jsondondur()
        {
            //Verilerin Json türünden döndürülmesini sağlar.
            //Farklı sistemlere çıktı vermek için kullanılabilir.
            //Class tipe veri aktar
            Calisan calisan1 = new Calisan()
            { Kimlikno = 5, Adsoyad = "Ali Aslan", Eposta = "ali@ali.com", Birimi = "Muhasebe" };
            return Json(calisan1);
        }

        [HttpGet]
        public FileResult Dosyaindir()
        {
            //Dosya (File) olarak bir dosyanın indirilmesini sağlayabilir.
            byte[] dosyayioku = System.IO.File.ReadAllBytes(@"wwwroot/bilgiler.txt");//Dosya okunuyor.
            string dosyaadi = "bilgileryeni.txt";
            return File(dosyayioku, System.Net.Mime.MediaTypeNames.Application.Octet, dosyaadi);
        }

        public PartialViewResult Altbilgiekle()
        {
            //Bir View içerisinde kullanılabilen bir parçanın yüklenmesini sağlar.
            //Tam sayfa yerine yalnızca bir parça görünümü döndürür.
            //Bu, genellikle Ajax istekleriyle birlikte kullanılabilir.
            return PartialView("_Altbilgi");//Views/shared/_Altbilgi Razor düzeni dosyasını döndür.
        }
    }

    public class Calisan
    {
        public int Kimlikno { get; set; }
        public string Adsoyad { get; set; }
        public string Eposta { get; set; }
        public string Birimi { get; set; }
    }
}
