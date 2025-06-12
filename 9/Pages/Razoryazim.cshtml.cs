using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class RazoryazimModel : PageModel
    {
        public void OnGet()
        {
            //Sayfa ilk y�klendi�inde �al��an olayd�r.
            ViewData["mesaj"] = "ASP.NET Razor, Microsoft taraf�ndan geli�tirilmi�, web sayfalar�nda sunucu tarafl� kodlar yazmak i�in kullan�lan bir yaz�m �ekli ve g�r�n�m motorudur. ";
        }
    }
}
