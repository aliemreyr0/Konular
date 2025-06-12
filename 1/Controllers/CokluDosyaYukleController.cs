using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BenimsiteMvc.Controllers
{
    public class CokluDosyaYukleController : Controller
    {
        // Dosyaların kaydedileceği klasör yolu için kullanıldı. Dosya oluşturma izni açık olmalıdır.
        private readonly string _dosyaYuklemeDizini;
        // Maksimum dosya boyutu: 2MB
        private const long _maksimumDosyaBoyutu = 2 * 1024 * 1024; // 2MB
        // İzin verilen dosya uzantıları
        private readonly string[] _izinVerilenUzantilar = { ".jpg", ".jpeg", ".png" };

        // Web hosting ortamını enjekte ediyoruz
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CokluDosyaYukleController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            // Dosyaların kaydedileceği klasör yolunu belirle
            _dosyaYuklemeDizini = Path.Combine(_hostingEnvironment.WebRootPath, "yukleme");

            // Klasör yoksa oluşturuyoruz. Geçerli izin olmalıdır.
            if (!Directory.Exists(_dosyaYuklemeDizini))
            {
                Directory.CreateDirectory(_dosyaYuklemeDizini);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CokluDosyaModel());
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF (Cross-Site Request Forgery) saldırılarına karşı korumak için kullanılır.
        public async Task<IActionResult> Index(CokluDosyaModel model)
        {
            // Model geçerli değilse (Yani dosya seçilmemişse)
            if (!ModelState.IsValid)
            {
                model.Mesaj = "Lütfen en az bir dosya seçiniz.";
                model.BasariliMi = false;
                return View(model);
            }
            // Dosya listesi boş ise
            if (model.Dosyalar == null || model.Dosyalar.Count == 0)
            {
                model.Mesaj = "Lütfen en az bir dosya seçiniz.";
                model.BasariliMi = false;
                return View(model);
            }
            // Her dosyayı kontrol et ve yükle
            foreach (var dosya in model.Dosyalar)
            {
                // Dosya boyutu kontrolü
                if (dosya.Length > _maksimumDosyaBoyutu)
                {
                    model.Mesaj = $"{dosya.FileName} dosyası çok büyük. Maksimum dosya boyutu 2Mb olmalıdır.";
                    model.BasariliMi = false;
                    return View(model);
                }
                // Dosya uzantısı kontrolü
                var uzanti = Path.GetExtension(dosya.FileName).ToLower();
                if (!_izinVerilenUzantilar.Contains(uzanti))
                {
                    model.Mesaj = $"Sadece {string.Join(", ", _izinVerilenUzantilar)} uzantılı dosyalar yüklenebilir.";
                    model.BasariliMi = false;
                    return View(model);
                }
                // Dosyayı benzersiz bir isimle kaydet. Dosyaları erişilmesi kolay olmasın diye
                // Benzersiz bir isimle kaydedebiliriz.
                var dosyaAdi = $"{Guid.NewGuid()}{uzanti}";
                var dosyaYolu = Path.Combine(_dosyaYuklemeDizini, dosyaAdi);

                // Dosyayı klasöre kaydet
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await dosya.CopyToAsync(stream);
                }
                // Başarıyla yüklenen dosya adını listeye ekle
                model.YuklenenDosyalar.Add(dosyaAdi);
            }
            // Tüm işlemler başarılıysa
            model.Mesaj = $"{model.YuklenenDosyalar.Count} dosya başarıyla yüklendi.";
            model.BasariliMi = true;
            return View(model);
        }

        // Yüklenen bir dosyayı görüntüle
        public IActionResult DosyaGoruntule(string dosyaAdi)
        {
            if (string.IsNullOrEmpty(dosyaAdi))
            {
                return NotFound();
            }
            var dosyaYolu = Path.Combine(_dosyaYuklemeDizini, dosyaAdi);
            if (!System.IO.File.Exists(dosyaYolu))
            {
                return NotFound();
            }
            // Dosya uzantısına göre content type belirle
            var uzanti = Path.GetExtension(dosyaAdi).ToLower();
            string iceriktturu = uzanti switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
            // Dosyayı oku ve dön
            var dosyaVerisi = System.IO.File.ReadAllBytes(dosyaYolu);
            return File(dosyaVerisi, iceriktturu);
        }
    }
}
