using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class FormisleModel : PageModel
    {
        //Özellikleri tanýtalým
        [BindProperty(SupportsGet = true)]//Eðer get istek türü kullanýlacaksa ve sorgu string'leri alýnacaksa bu direktif verilir.
        public string Renk { get; set; }
        public string Mesaj { get; set; }
        public void OnGet()
        {
            //Get istek türü oluþtuðunda çalýþan olaydýr. Ýlk sayfa yüklendiðinde varsayýlan istek türü Get türüdür.
            Mesaj = "OnGet olayý tetiklendi. Þu rengi seçtiniz:" + Renk + " Týklanma Tarihi:" + DateTime.Now.ToString();
        }

        public void OnPost()
        {
            //Post istek türü oluþtuðunda meydana gelir
            Mesaj = "Gönder düðmesi ile OnPost eylemi tetiklendi";
        }

        public void OnPostDuzelt()
        {
            //button etiketinin asp-page-handler="Duzelt" olan düðme týklandýðýnda meydana gelir.
            Mesaj = "OnPostDuzelt eylemi tetiklendi";
        }

        public void OnPostGor()
        {
            //button etiketinin asp-page-handler="Gor" olan düðme týklandýðýnda meydana gelir.
            Mesaj = "OnPostGor eylemi tetiklendi";
        }
        public void OnPostSil()
        {
            //button etiketinin asp-page-handler="Sil" olan düðme týklandýðýnda meydana gelir.
            Mesaj = "OnPostSil eylemi tetiklendi";
        }

    }
}
