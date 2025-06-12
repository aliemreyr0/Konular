using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BenimsiteMvc.Controllers
{
    public class UyeekleController : Controller
    {
        /*
         * Kullanicilar tablosu için Sql kodu:. Veritabanında sağ tuş>New Query ile çalıştırılır
        CREATE TABLE kullanicilar (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Kuladi VARCHAR(10) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NOT NULL,
            Parola VARCHAR(100) NOT NULL,
            Adsoyad VARCHAR(25) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NULL,
            Eposta VARCHAR(50) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NULL,
            Yetki INT NULL DEFAULT 0,
            Aciklama VARCHAR(200) COLLATE Latin1_General_100_CI_AS_SC_UTF8 NULL,
            Ktarihi DATETIME DEFAULT GETDATE()
        );
        */
        private readonly string _baglanticumlesi;

        public UyeekleController(IConfiguration configuration)
        {
            _baglanticumlesi = configuration.GetConnectionString("Baglanti1");
        }

        public IActionResult Index()
        {
            // Yetki kontrolü
            var yetki = HttpContext.Session.GetString("Yetki");
            if (yetki != "-1")
            {
                return RedirectToAction("Index", "UyeGirisi");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(string Kuladi, string Parola, string Adsoyad, string Eposta, string Aciklama, string Yetki)
        {
            var yetki = HttpContext.Session.GetString("Yetki");
            if (yetki != "-1")
            {
                return RedirectToAction("Index", "UyeGirisi");
            }

            using var baglanti = new SqlConnection(_baglanticumlesi);
            baglanti.Open();
            if (UyeVarMi(baglanti, Kuladi, "Kuladi"))
            {
                ViewData["sonucmesaj"] = "<div class='alert alert-danger'>Bu kullanıcı adı zaten kayıtlı.</div>";
                return View();
            }

            if (UyeVarMi(baglanti, Eposta, "Eposta"))
            {
                ViewData["sonucmesaj"] = "<div class='alert alert-danger'>Bu e-posta zaten kayıtlı.</div>";
                return View();
            }

            string sql = @"INSERT INTO kullanicilar (Kuladi, Parola, Adsoyad, Eposta, Aciklama, Yetki)
                           VALUES (@Kuladi, @Parola, @Adsoyad, @Eposta, @Aciklama, @Yetki)";
            using var komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@Kuladi", Kuladi);
            komut.Parameters.AddWithValue("@Parola", Ayarlar.Md5ilesifrele(Parola));
            komut.Parameters.AddWithValue("@Adsoyad", Adsoyad);
            komut.Parameters.AddWithValue("@Eposta", Eposta);
            komut.Parameters.AddWithValue("@Aciklama", Aciklama);
            komut.Parameters.AddWithValue("@Yetki", Yetki);
            try
            {
                komut.ExecuteNonQuery();
                ViewData["sonucmesaj"] = "<div class='alert alert-success'>Kullanıcı başarıyla eklendi.</div>";
            }
            catch (Exception hata)
            {
                ViewData["sonucmesaj"] = $"<div class='alert alert-danger'>Hata: {hata.Message}</div>";
            }

            return View();
        }

        private bool UyeVarMi(SqlConnection baglanti, string veri, string alan)
        {
            string sql = $"SELECT 1 FROM kullanicilar WHERE {alan} = @veri";
            using var komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@veri", veri);
            using var oku = komut.ExecuteReader();
            return oku.HasRows;
        }
    }
}
