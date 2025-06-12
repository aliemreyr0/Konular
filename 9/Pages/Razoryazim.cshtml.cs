using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class RazoryazimModel : PageModel
    {
        public void OnGet()
        {
            //Sayfa ilk yüklendiðinde çalýþan olaydýr.
            ViewData["mesaj"] = "ASP.NET Razor, Microsoft tarafýndan geliþtirilmiþ, web sayfalarýnda sunucu taraflý kodlar yazmak için kullanýlan bir yazým þekli ve görünüm motorudur. ";
        }
    }
}
