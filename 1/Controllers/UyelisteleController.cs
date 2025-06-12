using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BenimsiteMvc.Controllers
{
    public class UyelisteleController : Controller
    {
        private readonly string _baglanticumlesi;

        public UyelisteleController(IConfiguration configuration)
        {
            _baglanticumlesi = configuration.GetConnectionString("Baglanti1");
        }
        public IActionResult Index()
        {
            // Yetki kontrolü (güvenlik açısından önemli)
            var yetki = HttpContext.Session.GetString("Yetki");
            if (yetki != "-1") // Sadece yöneticiler görebilir
            {
                return RedirectToAction("Index", "UyeGirisi");
            }
            //Kullanıcılar class tipi için List oluştur. Bu list'i view'e gönder.
            List<Kullanici> kullanicilar = new List<Kullanici>();
            using (SqlConnection baglanti = new SqlConnection(_baglanticumlesi))
            {
                baglanti.Open();
                string sql = "SELECT * FROM kullanicilar";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    kullanicilar.Add(new Kullanici
                    {
                        Id = (int)oku["Id"],
                        Kuladi = oku["Kuladi"].ToString(),
                        Adsoyad = oku["Adsoyad"].ToString(),
                        Eposta = oku["Eposta"].ToString(),
                        Yetki = oku["Yetki"].ToString(),
                        Aciklama = oku["Aciklama"].ToString()
                    });
                }
            }
            return View(kullanicilar);
        }
    }

    public class Kullanici
    {
        public int Id { get; set; }
        public string Kuladi { get; set; }
        public string Adsoyad { get; set; }
        public string Eposta { get; set; }
        public string Yetki { get; set; }
        public string Aciklama { get; set; }
    }

}
