using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class SessionOlusturController : Controller
    {
        public IActionResult Index()
        {
            //Session oluşturma işleminden önce uygulamada Startup.cs dosyasında genel bir ayarlama yapılmalıdır.
            //Aksi halde hata meydana gelecektir.
            try
            {
                //Session (Oturum) değişkenini oluştur.
                HttpContext.Session.SetString("adsoyad", "Mustafa OF");
                HttpContext.Session.SetString("tarihsaat", DateTime.Now.ToString());
            }
            catch (Exception hata)
            {
                ViewData["sonucmesaj"] = "Hata oluştu. Hata:" + hata.Message;
                return View();
            }
            ViewData["sonucmesaj"] = "Oturum Değişlenleri Oluşturuldu. Değerleri :" +
                "Adı Soyadı:" + HttpContext.Session.GetString("adsoyad") +
                " Oluşturulma Tarih/Saati:" + HttpContext.Session.GetString("tarihsaat") +
                " Oturum Kimliği:" + HttpContext.Session.Id;
            return View();
        }
    }
}
