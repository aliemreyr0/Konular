using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class BilgigonderModel : PageModel
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
            string dosyaadi = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\dosyalar\bilgigonder.txt");
            //Dosya, yazma iznine karþý açýk olmalýdýr.
            try
            {
                StreamWriter dosyayaz = new StreamWriter(dosyaadi, true, System.Text.Encoding.GetEncoding("utf-8"));
                String ayrac = "\r\n";//Alt satýra indirir
                string ipadresi = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                //Verileri dosyaya yaz
                dosyayaz.Write("Adý Soyadý:" + Adsoyad + ayrac + "E-Posta Adresi:" + Eposta + ayrac +
                    "Tahsili:" + Tahsili + ayrac + "Verilen Oy:" + Oy + ayrac + "Mesaj:" + Mesaj + ayrac +
                    "Ip Adresi:" + ipadresi + ayrac + "Ýþlem Tarihi:" + DateTime.Now.ToString() + ayrac);
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
