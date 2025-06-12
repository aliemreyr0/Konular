using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    
    public class Sayfa2Model : PageModel
    {
        public string mesaj { get; set; } //Genel de�i�ken
        public void OnGet()
        {
            //Sayfa ilk y�klendi�inde �al��an olayd�r.
            Random rasgele = new Random();
            string[] selamcumleleri = {"�yi �al��malar","Merhaba","�yi Ak�amlar","Kolay Gelsin","Ho� Geldiniz" };
            mesaj = selamcumleleri[rasgele.Next(0, selamcumleleri.Length)];//Diziden rasgele bir c�mle se�iyor.
            //Razor sayfas�nda mesaj g�sterildi�i i�in sayfada g�r�n�yor
        }
    }
}
