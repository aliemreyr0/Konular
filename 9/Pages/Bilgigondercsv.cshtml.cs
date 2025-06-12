using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class BilgigondercsvModel : PageModel
    {
        //Özellikleri hazýrlayalým
        [BindProperty] public string Adsoyad { get; set; }
        [BindProperty] public string Eposta { get; set; }
        [BindProperty] public string Tahsili { get; set; }
        [BindProperty] public string Oy { get; set; }
        [BindProperty] public string Mesaj { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            //Ýstek türü post ise. <form method="post"></form>
            //Gönder düðmesine basýldýðýnda tetiklenir.
            //Dosya, yazma iznine karþý açýk olmalýdýr.
            string dosyaadi = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\dosyalar\bilgigonder.csv");            
            try
            {
                StreamWriter dosyayaz = new StreamWriter(dosyaadi, true, System.Text.Encoding.GetEncoding("utf-8"));
                String ayrac = ";";//Csv formatý için ; ayracý kullanýlýr.
                string ipadresi = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                //Verileri dosyaya yaz
                dosyayaz.Write(Adsoyad + ayrac + Eposta + ayrac +
                    Tahsili + ayrac + Oy + ayrac + Mesaj + ayrac +
                    ipadresi + ayrac + DateTime.Now.ToString() + "\r\n");
                dosyayaz.Close();//Dosyayý kapat
            }
            catch (Exception hata)
            {
                ViewData["Durummesaji"] = "Hata Oluþtu. Lütfen tekrar deneyiniz. Hata:" + hata.Message;
                return;
            }
            ViewData["Durummesaji"] = "Bilgileriniz için teþekkürler.";
        }
    }
}
