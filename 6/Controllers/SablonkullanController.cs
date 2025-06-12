using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class SablonkullanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Akademik()
        {
            return View();
        }

        public IActionResult Iletisim()
        {
            return View();
        }

        public IActionResult Belgeler()
        {
            return View();
        }

        public IActionResult Mezunlar()
        {
            return View();
        }
    }
}
