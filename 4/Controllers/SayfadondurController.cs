using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class SayfadondurController : Controller
    {
        public RedirectResult Index()
        {
            return Redirect("~/KampanyaVt2/Index");//Bu action'a yönlendirme yapar.
        }

        public JsonResult JsonDondur()
        {
            //Json verisi oluşturalım
            var veri = new { Isim = "Ali", Yas = 40 };
            return Json(veri);
        }

        public ContentResult IcerikDondur()
        {
            //ContentResult 
            string donen = "<h2>Merhaba, bu içerik ContentResult ile döndürüldü</h2>";
            return Content(donen,"text/html",System.Text.Encoding.UTF8);
        }

        public FileResult Dosyaindir()
        {
            //Dosya (File) olarak bir dosyanın indirilmesini sağlayabilir.
            byte[] dosyayioku = System.IO.File.ReadAllBytes(@"wwwroot/bilgiler.txt");//Dosya okunuyor.
            string dosyaadi = "bilgileryeni.txt";
            return File(dosyayioku, System.Net.Mime.MediaTypeNames.Application.Octet, dosyaadi);
        }
        public PartialViewResult Altbilgidondur()
        {
            //Bir View içerisinde kullanılabilen bir parçanın yüklenmesini sağlar.
            //Tam sayfa yerine yalnızca bir parça görünümü döndürür.
            //Bu, genellikle Ajax istekleriyle birlikte kullanılabilir.
            return PartialView("_Altbilgi");//Views/shared/_Altbilgi Razor düzeni dosyasını döndür.
        }
    }
}
