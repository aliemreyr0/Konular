using System.Diagnostics;
using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Ayarlar ayarlar = new Ayarlar();
            ViewData["Trtarihyaz"] = ayarlar.Trtarihyaz();//Ayarlar class'�ndaki metot �a�r�l�yor
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Bilgi()
        {
            return View();//Kullan�c�ya Html sayfas� d�nd�r�r
        }

        public string Selam()
        {
            return "Merhaba D�nya";//Bir metin d�nd�r�r
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
