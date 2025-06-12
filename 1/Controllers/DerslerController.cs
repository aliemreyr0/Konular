using BenimsiteMvc.Data;
using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenimsiteMvc.Controllers
{
    /*
    Ef Core ile Dersler Crud uygulaması 
    Nuget ile yüklenecek kitaplıklar:
    Microsoft.EntityFrameworkCoreSqlServer
    Microsoft.EntityFrameworkCoreTools paketleri yüklendi       
    1. Ders Class'ını Models klasöründe oluşturun. (Ekle>Sınıf)
    2. VeritabaniContext sınıfını Data klasöründe oluşturun. Kodlar ilgili sınıftadır.(Ekle>Sınıf)
    Veri tabanı bağlantı cümlesi appsettings.json'da doğru olarak girilir.
    3. Araçlar>Nuget Paket Yöneticisi>Paket Yöneticisi Konsolü ne geçilir.
    Komut satırında iken şu komutlar çalıştırılır ve Veri tabanı, Dersler tablosu oluşmuş olur.
    Add-Migration DersTablosu (Eğer hata verirse Remove-Migration ile kaldırılır)
    Update-Database
    4. Ekle>Denetleyici>Entity Framework... seçilir
    Gelen pencerede Model Sınıfı: Dersler, DbContext: VeritabaniContext, Denetleyici adı: DerslerController
    Ekle düğmesi ile DerslerController ve Crud işlemlerinin Action görünümleri (View) Visual Studio tarafından otomatik oluşur.
    İstenirse View'ler veya DerslerController dosyalarında değişiklikler yapılır.    
    */
    public class DerslerController : Controller
    {
        private readonly VeritabaniContext _context;

        public DerslerController(VeritabaniContext context)
        {
            _context = context;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dersler.ToListAsync());
        }

        // GET: Dersler/Gor/5
        public async Task<IActionResult> Gor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Dersler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ders == null)
            {
                return NotFound();
            }
            return View(ders);
        }

        // GET: Dersler/Ekle
        public IActionResult Ekle()
        {
            return View();
        }

        // POST: Dersler/Ekle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle([Bind("Id,DersAdi,Hoca,Kredi,Aciklama")] Ders ders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ders);
        }

        // GET: Dersler/Duzenle/5
        public async Task<IActionResult> Duzenle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Dersler.FindAsync(id);
            if (ders == null)
            {
                return NotFound();
            }
            return View(ders);
        }

        // POST: Dersler/Duzenle/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(int id, [Bind("Id,DersAdi,Hoca,Kredi,Aciklama")] Ders ders)
        {
            if (id != ders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DersVarmi(ders.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ders);
        }

        // GET: Dersler/Sil/5
        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ders = await _context.Dersler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ders == null)
            {
                return NotFound();
            }

            return View(ders);
        }

        // POST: Dersler/Sil/5
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilmeOnayi(int id)
        {
            var ders = await _context.Dersler.FindAsync(id);
            if (ders != null)
            {
                _context.Dersler.Remove(ders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DersVarmi(int id)
        {
            return _context.Dersler.Any(e => e.Id == id);//Varsa true döndür
        }
    }
}
