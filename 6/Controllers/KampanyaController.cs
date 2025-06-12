using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class KampanyaController : Controller
    {
        //Formdan gelen verileri parametre olarak alalım
        public IActionResult Index(string Adsoyad, string Tcno, string Eposta, string Kampanyano)
        {
            //Girilen verileri Csv Excel dosyası olarak kaydeder.
            //Boşluk kontrolü
            if (Adsoyad != null || Tcno != null || Eposta != null || Kampanyano != null)
            {
                string dosyaadi = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\dosyalar\kampanya.csv");
                //kampanya.csv dosyasına public kullanıcısı için yazma izni (write) olmalıdır.
                string ayrac = ";";//Csv ayracı
                string ipadresi = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                try
                {
                    StreamWriter dosyayaz = new StreamWriter(dosyaadi, true, System.Text.Encoding.UTF8);
                    dosyayaz.WriteLine(Adsoyad + ayrac + Tcno + ayrac + Eposta + ayrac + Kampanyano + ayrac + DateTime.Now + ayrac + ipadresi);
                    dosyayaz.Close();//Kapat
                }
                catch (Exception hata)
                {
                    ViewData["sonucmesaj"] = "Hata oluştu. Lütfen tekrar deneyiniz. Hata:" + hata.Message;
                    return View();//Bir View döndürmeliyiz.
                }
                ViewData["sonucmesaj"] = "Bilgileriniz için teşekkürler";
                return View();
            }//if
            return View();
        }
    }
}
