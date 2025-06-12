using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    
    public class Sayfa2Model : PageModel
    {
        public string mesaj { get; set; } //Genel deðiþken
        public void OnGet()
        {
            //Sayfa ilk yüklendiðinde çalýþan olaydýr.
            Random rasgele = new Random();
            string[] selamcumleleri = {"Ýyi Çalýþmalar","Merhaba","Ýyi Akþamlar","Kolay Gelsin","Hoþ Geldiniz" };
            mesaj = selamcumleleri[rasgele.Next(0, selamcumleleri.Length)];//Diziden rasgele bir cümle seçiyor.
            //Razor sayfasýnda mesaj gösterildiði için sayfada görünüyor
        }
    }
}
