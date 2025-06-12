using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class PersonelListeleController : Controller
    {
        //Personel isimli Models'de bulunan sınıfa ait bilgileri oluşturur ve listeler.
        List<Personel> personeller = new List<Personel>
        {
            new Personel {Id=1,Adsoyad = "Ali Aslan", Yas = 34},
            new Personel {Id=2, Adsoyad = "Kemal Kadim", Yas = 44},
            new Personel {Id=3, Adsoyad = "Ayşe Ak", Yas = 54},
            new Personel {Id=4, Adsoyad = "Zeki Al", Yas = 24}
        };

        public IActionResult Index()
        {
            //personelle isimli dizi listesi, View'e gönderildi.
            return View(personeller);
        }
    }
}
