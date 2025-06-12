using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class FormisleModel : PageModel
    {
        //�zellikleri tan�tal�m
        [BindProperty(SupportsGet = true)]//E�er get istek t�r� kullan�lacaksa ve sorgu string'leri al�nacaksa bu direktif verilir.
        public string Renk { get; set; }
        public string Mesaj { get; set; }
        public void OnGet()
        {
            //Get istek t�r� olu�tu�unda �al��an olayd�r. �lk sayfa y�klendi�inde varsay�lan istek t�r� Get t�r�d�r.
            Mesaj = "OnGet olay� tetiklendi. �u rengi se�tiniz:" + Renk + " T�klanma Tarihi:" + DateTime.Now.ToString();
        }

        public void OnPost()
        {
            //Post istek t�r� olu�tu�unda meydana gelir
            Mesaj = "G�nder d��mesi ile OnPost eylemi tetiklendi";
        }

        public void OnPostDuzelt()
        {
            //button etiketinin asp-page-handler="Duzelt" olan d��me t�kland���nda meydana gelir.
            Mesaj = "OnPostDuzelt eylemi tetiklendi";
        }

        public void OnPostGor()
        {
            //button etiketinin asp-page-handler="Gor" olan d��me t�kland���nda meydana gelir.
            Mesaj = "OnPostGor eylemi tetiklendi";
        }
        public void OnPostSil()
        {
            //button etiketinin asp-page-handler="Sil" olan d��me t�kland���nda meydana gelir.
            Mesaj = "OnPostSil eylemi tetiklendi";
        }

    }
}
