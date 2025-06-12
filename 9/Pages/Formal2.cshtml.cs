using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class Formal2Model : PageModel
    {
        [BindProperty] public string Adsoyad { get; set; }
        [BindProperty] public string Numara { get; set; }
        [BindProperty] public string Eposta { get; set; }
        [BindProperty] public string Bolumu { get; set; }
        [BindProperty] public string Mesaj { get; set; }
        public void OnGet()
        {
            //Sayfa yüklendiðinde ilk çalýþan olaydýr.
        }
    }
}
