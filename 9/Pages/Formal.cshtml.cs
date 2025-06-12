using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class FormalModel : PageModel
    {
        //Statik taraftan gelen bilgilerin �zellik de�erlerini tan�tal�m
        [BindProperty] public string Adsoyad { get; set; }
        [BindProperty] public string Eposta { get; set; }
        [BindProperty] public string Tcno { get; set; }
        public void OnGet()
        {
            //Sayfa y�klendi�inde ilk �al��an olayd�r.
        }
    }
}
