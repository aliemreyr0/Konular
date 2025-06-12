using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class UyecikisiController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();//Tüm oturum değişkenlerini sil
            return Redirect("/Uyegirisi");//Üye giriş sayfasına yönlendir.
        }
    }
}
