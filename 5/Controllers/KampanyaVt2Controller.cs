using BenimsiteMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;//Nuget paket yöneticisinden "Microsoft.Data.SqlClient" paketi yüklenmelidir.


namespace BenimsiteMvc.Controllers
{
    public class KampanyaVt2Controller : Controller
    {        

        [HttpGet]
        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        //Parametrede KampanyaVt2GirisModel bilgisi verilmelidir.
        public IActionResult Index(KampanyaVt2GirisModel model)
        {
            //Formdan gelen veriler, model parametresi aracılığıyla işlenecektir.
            if (ModelState.IsValid) //Eğer Model aracılığı ile veri giriş kontrolü başarılı olarak geçildiyse...
            {
                //Boşluk kontrolü
                if (model.Adsoyad != null | model.Tcno != null | model.Eposta != null | model.Kampanyano != null)
                {
                    try
                    {
                        Ayarlar.Baglan();
                        //Tablo yoksa yeniden oluştur
                        string sql = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Kampanyalar' and xtype='U')" +
                            " CREATE TABLE Kampanyalar (id int IDENTITY(1, 1) NOT NULL primary key, Adsoyad varchar(25) null," +
                            " Eposta varchar(40) null, Tcno numeric null, Kampanyano int null, Ipadresi varchar(20) null, Ktarihi datetime default CURRENT_TIMESTAMP);";
                        SqlCommand komut = new SqlCommand(sql, Ayarlar.baglanti);
                        komut.ExecuteNonQuery();

                        //Tc nosu daha önce var olan kayıt için uyarı verme
                        string sql1 = "Select * from Kampanyalar Where Tcno=@Tcno;";
                        SqlCommand komut1 = new SqlCommand(sql1, Ayarlar.baglanti);
                        komut1.Parameters.AddWithValue("@Tcno", model.Tcno);
                        SqlDataReader oku = komut1.ExecuteReader();
                        if (oku.HasRows) //Eğer şarta uyan bir kayıt varsa
                        {
                            //Eğer aynı Tcno'dan varsa uyarı ver
                            ViewData["sonucmesaj"] = "Bu T.C. Numarası ile daha önce giriş yapılmış.";
                            return View();//Çık 
                        }
                        oku.Close();

                        //Yeni kayıt ekleme
                        string sql2 = "Insert into Kampanyalar (Adsoyad,Tcno,Eposta,Kampanyano,Ipadresi)";
                        sql2 += " Values (@Adsoyad,@Tcno,@Eposta,@Kampanyano,@Ipadresi);";
                        SqlCommand komut2 = new SqlCommand(sql2, Ayarlar.baglanti);
                        //Parametrelere bilgi geçme
                        //Parametre geçişi sağlayarak SQL Injection’a karşı korunuruz
                        komut2.Parameters.AddWithValue("@Adsoyad", model.Adsoyad);//XSS (Cross-site scripting) için Razor engine çıktıyı encode eder. Zararlı kodları engeller.
                        komut2.Parameters.AddWithValue("@Tcno", model.Tcno);
                        komut2.Parameters.AddWithValue("@Eposta", model.Eposta);
                        komut2.Parameters.AddWithValue("@Kampanyano", model.Kampanyano);
                        string ipadresi = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        komut2.Parameters.AddWithValue("@Ipadresi", ipadresi);
                        int eklenen = 0;
                        eklenen = komut2.ExecuteNonQuery();
                    }
                    catch (Exception hata)
                    {
                        ViewData["sonucmesaj"] = "Hata oluştu. Tekrar deneyiniz. Hata:" + hata.Message;
                        return View();//Görünüme geri dön
                    }
                    ViewData["sonucmesaj"] = "Bilgileriniz için teşekkürler";
                    return View();
                }//if
                return View();
            }
            else
            {
                ViewData["sonucmesaj"] = "Formda eksik veya hatalı alanlar var.";
            }

            return View(model);
        }
    }
}
