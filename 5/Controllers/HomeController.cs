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
            ViewData["Trtarihyaz"] = ayarlar.Trtarihyaz();//Ayarlar class'ýndaki metot çaðrýlýyor
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Bilgi()
        {
            return View();//Kullanýcýya Html sayfasý döndürür
        }

        public string Selam()
        {
            return "Merhaba Dünya";//Bir metin döndürür
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
