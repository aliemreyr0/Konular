using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BenimsiteMvc.Controllers
{
    public class UyeGirisiController : Controller
    {
        //Genel Değişken
        SqlConnection baglanti = null;
        private readonly string _baglanticumlesi;        
        public UyeGirisiController(IConfiguration ayarlama)
        {
            //appsettings.json'dan veri tabanı bağlantısını al
            _baglanticumlesi = ayarlama.GetConnectionString("Baglanti1");
        }
        
        public IActionResult Index()
        {           
            return View();
        }
        [HttpPost]
        public IActionResult Index(string Kuladi, string Parola)
        {            
            Baglan();//Veri tabanına bağlan
            //Kullanıcı adı ve şifre eşleştirmesi yapılıyor.
            string sql = "Select * from kullanicilar Where Kuladi=@Kuladi And Parola=@Parola";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@Kuladi", Kuladi);
            komut.Parameters.AddWithValue("@Parola", Ayarlar.Md5ilesifrele(Parola));
            //Şifre, Md5 olarak kayıtlı ise veri alınırken MD5'e çevrilmelidir. Md5ilesifrele, Ayarlar.cs içerisinde bulunmaktadır.
            SqlDataReader oku = null;
            oku = komut.ExecuteReader();
            oku.Read();//Bir kayıt oku
            if (oku.HasRows)//Eğer select satırından dönen bir satır varsa
            {
                //Giriş başarılı
                //Oturum değişkenlerini oluştur
                HttpContext.Session.SetString("Kulid", oku["Id"].ToString());
                HttpContext.Session.SetString("Kuladi", Kuladi);
                HttpContext.Session.SetString("Adsoyad", oku["Adsoyad"].ToString());
                HttpContext.Session.SetString("Eposta", oku["Eposta"].ToString());
                HttpContext.Session.SetString("Aciklama", oku["Aciklama"].ToString());
                HttpContext.Session.SetString("Yetki", oku["Yetki"].ToString());
                HttpContext.Session.SetString("Girissaati", DateTime.Now.ToString());
                return Redirect("/Uyeanasayfa");//Yönlendir
            }
            else
            {
                //Giriş başarısız
                ViewData["sonucmesaj"] = "<div class='alert alert-danger'>Kullanıcı ve şifre bilgileri eşleşmiyor. Tekrar Deneyiniz.</div>";
            }
            return View();
        }

        private void Baglan()
        {
            //Veri tabanına bağlan            
            baglanti = new SqlConnection(_baglanticumlesi);
            baglanti.Open();//Bağlantıyı aç
        }
    }
}
