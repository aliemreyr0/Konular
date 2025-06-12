using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace BenimsiteMvc.Controllers
{
    public class KampanyaVtListeleController : Controller
    {
        // Index metodunda veriyi View'a gönderiyoruz
        [HttpGet]
        public IActionResult Index()
        {
            var kampanyalar = new List<Dictionary<string, object>>();

            try
            {
                // Bağlantıyı aç
                Ayarlar.Baglan();

                // Veritabanı sorgusunu oluştur
                string query = "SELECT * FROM Kampanyalar";
                SqlCommand komut = new SqlCommand(query, Ayarlar.baglanti);

                // Sorguyu çalıştır ve verileri al
                using (var okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        //Kayıtları Dictionary nesnesine aktar
                        var kampanya = new Dictionary<string, object>
                        {
                            { "id", okuyucu["id"] },
                            { "adsoyad", okuyucu["Adsoyad"] },
                            { "eposta", okuyucu["Eposta"] },
                            { "tcno", okuyucu["Tcno"] },
                            { "kampanyano", okuyucu["Kampanyano"] },
                            { "ipadresi", okuyucu["Ipadresi"] },
                            { "ktarihi", okuyucu["Ktarihi"] }
                        };
                        kampanyalar.Add(kampanya);
                    }
                }
            }
            catch (Exception hata)
            {
                // Hata yönetimi
                return Json(new { success = false, message = "Hata oluştu: " + hata.Message });
            }

            // Verileri View'a gönderiyoruz
            return View(kampanyalar);
        }
    }
}
