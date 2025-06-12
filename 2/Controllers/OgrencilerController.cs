using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class OgrencilerController : Controller
    {
        public IActionResult Index()
        {
            ViewData["baslik"] = "Tüm Öğrenciler";
            OgrenciDbIsle ogrencidbisle1 = new OgrenciDbIsle();
            ModelState.Clear();//Model'i temizle
            return View(ogrencidbisle1.Ogrencilerigetir());//Listeleme için bu metot kullanılır.
        }

        //Yeni kayıt ekleme
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(OgrenciModel liste1)
        {
            //Eğer model uyumlu ise
            if (ModelState.IsValid)
            {
                OgrenciDbIsle ogrencidbisle1 = new OgrenciDbIsle();
                if (ogrencidbisle1.Kayitekle(liste1))
                {
                    ViewData["sonucmesaj"] = "Kayıt eklendi";
                    ModelState.Clear();
                }//if
            }
            return View();
        }
    }
}
