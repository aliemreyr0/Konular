using Microsoft.Data.SqlClient;
//Nuget paket yöneticisi "Microsoft.Data.SqlClient" aranarak yüklenir.
//localdb'nin bilgileri Araçlar-Add Sql Server ile görülebilir. 
//Eğer yüklü değilse Araçlar-Araçlar ve özellikler edinin düğmesi ile "Bağımsız bileşenler" kısmından "localdb" aranarak bulunur seçilir ve yüklenir.

namespace BenimsiteMvc.Controllers
{
    public class Ayarlar
    {
        //Projede çeşitli veri döndüren metotların paylaşıldığı class'dır.
        
        public Ayarlar()
        {
            //Oluşturucu metot
        }

        //Veri tabanı bağlantı cümlesi
        //Varsayılan veri tabanı adı "Veriler1" dir. Bu veri tabanının daha önceden yerel bilgisayarda oluşması gereklidir.
        public static string bcumle = @"Server=(localdb)\MSSQLLocalDb;Database=Veriler1;";

        public static SqlConnection baglanti = null;//Bağlantı bilgilerini alacak

        public static void Baglan()
        {
            //Veri tabanına bağlan            
            baglanti = new SqlConnection(bcumle);
            baglanti.Open();//Bağlantıyı aç
        }
        public String Trtarihyaz()
        {
            //Zaman dilimini Türkiye olarak ayarlar ve tarih/zamanı döndürür.
            //UTC zamanını al
            DateTime utctarihzaman = DateTime.UtcNow;

            // Saat dilimini Türkiye'ye uyarla
            TimeZoneInfo TrZamanDilimi = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

            // UTC zamanını Türkiye zaman dilimine çevir
            DateTime Trtarihzaman = TimeZoneInfo.ConvertTimeFromUtc(utctarihzaman, TrZamanDilimi);
            String donen = Trtarihzaman.ToLongDateString() + " " + Trtarihzaman.ToLongTimeString();
            return donen;
        }
    }
}
