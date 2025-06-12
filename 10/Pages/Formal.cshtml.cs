using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class FormalModel : PageModel
    {
        //Statik taraftan gelen bilgilerin özellik deðerlerini tanýtalým
        [BindProperty] public string Adsoyad { get; set; }
        [BindProperty] public string Eposta { get; set; }
        [BindProperty] public string Tcno { get; set; }
        public void OnGet()
        {
            //Sayfa yüklendiðinde ilk çalýþan olaydýr.
        }
    }
}
