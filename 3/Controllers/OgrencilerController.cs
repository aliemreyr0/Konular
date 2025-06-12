using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class OgrencilerController : Controller
    {
        //Models klasöründe Ogrencimodel oluşturulmalıdır. Alanlar ve sınırlamalar yazılmalıdır.
        //CRUD (Create, Read, Update, Delete) işlemlerinin görünümlerini hazırlayan denetleyici
        //İlk açılan Index sayfasında tüm ürünleri gösterir (list).
        //Denetleyici hazırlandıktan sonra CRUD görünümlerini hazırlamak için Eylemler (Action) üzerinde sağ tıklayarak
        //Görünüm ekle>Razor seçilir. Örneğin Index için Şablon "List" seçilir.
        //Diğer şablonlar seçilir. Örneğin Yeni kayıt ekleme eylemi için şablon olarak "Create" seçilir.
        //Diğer şablonlar sırayla oluşturulabilir. Edit, Delete, List
        //Model sınıfı: "Ogrencimodel"
        //Razor sayfası oluştuktan sonra Html kodlarında gerekli değişiklikler yapılır.
        //Veri tabanı bağlantı cümlesi, appsettings.json dan alınmıştır. Bu dosyada "Baglanti1" adlı cümle olmalıdır.
        public IActionResult Index()
        {
            ViewData["baslik"] = "Tüm Öğrenciler";
            OgrenciDbIsle ogrencidbisle1 = new OgrenciDbIsle();
            ModelState.Clear();//Model'i temizle
            return View(ogrencidbisle1.Kayitlarigetir());//Listeleme için bu metot kullanılır.
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
            if (ModelState.IsValid)
            {
                OgrenciDbIsle ogrencidbisle1 = new OgrenciDbIsle();

                // Aynı öğrenci numarası var sa kontrol et
                if (ogrencidbisle1.OgrencinoVarmi(liste1.Ogrencino))
                {
                    ViewData["sonucmesaj"] = "Bu öğrenci numarası zaten kayıtlı!.";
                    return View();
                }

                if (ogrencidbisle1.Kayitekle(liste1))
                {
                    ViewData["sonucmesaj"] = "Kayıt Eklendi";
                    ModelState.Clear();
                }
            }
            return View();
        }

        //Kaydı güncelleme
        [HttpGet]
        public ActionResult Edit(int Id) //Düzeltilecek kayıt içeriği gösterilir
        {
            OgrenciDbIsle ogrencidbisle1 = new OgrenciDbIsle();
            return View(ogrencidbisle1.Kayitlarigetir().Find(ogrencimodeli => ogrencimodeli.Id == Id));
            //Linq satırı ile Id eşleştirmesini yapar ve uygun kaydı getirir.
        }
        [HttpPost]
        public ActionResult Edit(int Id, OgrenciModel liste1) //Değişiklikler alınır ve kaydedilir.
        {
            try
            {
                OgrenciDbIsle ogrencidbisle1 = new OgrenciDbIsle();
                // Bu Ogrencino başka bir kayıtta var mı?
                if (ogrencidbisle1.OgrencinoVarmi(liste1.Ogrencino, Id))
                {
                    ViewData["sonucmesaj"] = "Bu öğrenci numarası başka bir öğrenciye ait!";
                    return View(liste1);
                }
                bool sonuc = ogrencidbisle1.Kayitbilgidegis(liste1);
                if (sonuc)
                {
                    ViewData["sonucmesaj"] = "Kayıt Güncellendi";
                }
                return View(liste1);
            }
            catch
            {
                return View(liste1);
            }
        }

        [HttpGet]
        public ActionResult Details(int Id) //Kayıt içeriği bir sayfada gösterilir.
        {
            OgrenciDbIsle ogrencidbisle1 = new OgrenciDbIsle();
            return View(ogrencidbisle1.Kayitlarigetir().Find(kayitmodeli => kayitmodeli.Id == Id));
            //Linq satırı ile Id eşleştirmesini yapar ve uygun ürünü getirir.
        }

        //Kayıtları silme
        public ActionResult Delete(int Id)
        {
            try
            {
                OgrenciDbIsle ogrencidbisle1 = new OgrenciDbIsle();
                if (ogrencidbisle1.Kayitsil(Id))
                {
                    return RedirectToAction("Index");//Index metoduna yönlendir
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
