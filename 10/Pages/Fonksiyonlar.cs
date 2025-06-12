namespace Benimsite2.Pages
{
    public class Fonksiyonlar
    {
        //Web sitesinde kullanılacak genel fonksiyonlar için bu dosya kullanılabilir.
        public String Trtarihyaz()
        {
            //Zaman dilimini Türkiye olarak ayarlar ve tarih/zamanı döndürür.
            // UTC zamanını al
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
