using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class UyeanasayfaController : Controller
    {
        public IActionResult Index()
        {
            //Uyegirisi denetleyicisinde oluşturulan oturum değişkenlerini al
            ViewData["Kulid"] = HttpContext.Session.GetString("Kulid");//Oturum değişkeninin anahtar bilgisi
            ViewData["Kuladi"] = HttpContext.Session.GetString("Kuladi");//Oturum değişkeninin anahtar bilgisi
            ViewData["Adsoyad"] = HttpContext.Session.GetString("Adsoyad");//Oturum değişkeninin anahtar bilgisi
            ViewData["Eposta"] = HttpContext.Session.GetString("Eposta");//Oturum değişkeninin anahtar bilgisi
            ViewData["Aciklama"] = HttpContext.Session.GetString("Aciklama");//Oturum değişkeninin anahtar bilgisi
            ViewData["Yetki"] = HttpContext.Session.GetString("Yetki");//Yetki alanı eğer -1 ise yönetici, değilse normal kullanıcı
            ViewData["Girissaati"] = HttpContext.Session.GetString("Girissaati");//Oturum değişkeninin anahtar bilgisi         
            return View();
        }
    }
}
