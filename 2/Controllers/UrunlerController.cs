using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class UrunlerController : Controller
    {
        //Models klasöründe urunlermodel oluşturulmalıdır. Alanlar ve sınırlamalar yazılmalıdır.
        //CRUD (Create, Read, Update, Delete) işlemlerinin görünümlerini hazırlayan denetleyici
        //İlk açılan Index sayfasında tüm ürünleri gösterir (list).
        //Denetleyici hazırlandıktan sonra CRUD görünümlerini hazırlamak için Eylemler (Action) üzerinde sağ tıklayarak
        //Görünüm ekle>Razor seçilir. Örneğin Index için Şablon "List" seçilir.
        //Diğer şablonlar seçilir. Örneğin Yeni kayıt ekleme eylemi için şablon olarak "Create" seçilir.
        //Diğer şablonlar sırayla oluşturulabilir. Edit, Delete, List
        //Model sınıfı: "Urunlermodel"
        //Razor sayfası oluştuktan sonra Html kodlarında gerekli değişiklikler yapılır.
        //Veri tabanı bağlantı cümlesi, appsettings.json dan alınmıştır. Bu dosyada "Baglanti1" adlı cümle olmalıdır.
        public IActionResult Index()
        {
            ViewData["baslik"] = "Veri Tabanındaki Ürünler";
            UrunDbIsle urundbisle1 = new UrunDbIsle();
            ModelState.Clear();//Model erişim bilgisini temizle
            return View(urundbisle1.Urunlerigetir());//Listeleme işlemi için Urunlerigetir metodu kullanılır.
        }

        //Yeni kayıt ekleme
        [HttpGet]
        public ActionResult Create()//Yeni kayıt ekleme
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UrunlerModel liste1)//Yeni kayıt ekleme
        {
            //Eğer model uyumlu ise
            if (ModelState.IsValid)
            {
                UrunDbIsle urundbisle1 = new UrunDbIsle();
                if (urundbisle1.Kayitekle(liste1))
                {
                    ViewData["sonucmesaj"] = "Kayıt eklendi";
                    ModelState.Clear();
                }
            }//if
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id) //Düzeltilecek kayıt içeriği gösterilir
        {
            UrunDbIsle urundbisle1 = new UrunDbIsle();
            return View(urundbisle1.Urunlerigetir().Find(urunmodeli => urunmodeli.Id == Id));
            //Linq satırı ile Id eşleştirmesini yapar ve uygun ürünü getirir.
        }
        [HttpPost]
        public ActionResult Edit(int Id, UrunlerModel liste1) //Değişiklikler alınır ve kaydedilir.
        {
            try
            {
                UrunDbIsle urundbisle1 = new UrunDbIsle();
                bool sonuc = urundbisle1.Urunbilgidegis(liste1);//Değişiklikleri güncelle
                if (sonuc)
                {
                    ViewData["sonucmesaj"] = "Kayıt güncellendi";
                }
                return RedirectToAction("Index");//Index sayfasına yönlendir
            }
            catch
            {
                return View();//Hata oluşursa görünümü göster
            }
        }

        [HttpGet]
        public ActionResult Details(int Id) //Kayıt içeriği bir sayfada gösterilir.
        {
            UrunDbIsle urundbisle1 = new UrunDbIsle();
            return View(urundbisle1.Urunlerigetir().Find(urunmodeli => urunmodeli.Id == Id));
            //Linq satırı ile Id eşleştirmesini yapar ve uygun ürünü getirir.
        }

        //Kayıtları silme
        public ActionResult Delete(int Id)
        {
            try
            {
                UrunDbIsle urundbisle1 = new UrunDbIsle();
                if (urundbisle1.Urunsil(Id))
                {
                    ViewData["sonucmesaj"] = "Kayıt silindi";
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
