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
            //Bu list'in amacı tablodan gelen verileri içinde barındırmaktır.
            var kampanyalar = new List<Dictionary<string, object>>();

            try
            {
                // Bağlantıyı aç
                Ayarlar.Baglan();

                // Veritabanı sorgusunu oluştur
                string sql = "SELECT * FROM Kampanyalar";
                SqlCommand komut = new SqlCommand(sql, Ayarlar.baglanti);

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
                // Hata çıktısını Json tarzında döndürebiliriz.
                return Json(new { success = false, message = "Hata oluştu: " + hata.Message });
            }

            // Verileri View'a gönderiyoruz
            return View(kampanyalar);//Oluşturulan verilerin bulunduğu Dictionary list'i view tarafına gönderiliyor.
        }
    }
}
