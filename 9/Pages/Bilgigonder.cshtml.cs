using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class BilgigonderModel : PageModel
    {
        //�zellikleri haz�rlayal�m
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
            //�stek t�r� post ise. <form method="post"></form>
            //G�nder d��mesine bas�ld���nda tetiklenir.
            string dosyaadi = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\dosyalar\bilgigonder.txt");
            //Dosya, yazma iznine kar�� a��k olmal�d�r.
            try
            {
                StreamWriter dosyayaz = new StreamWriter(dosyaadi, true, System.Text.Encoding.GetEncoding("utf-8"));
                String ayrac = "\r\n";//Alt sat�ra indirir
                string ipadresi = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                //Verileri dosyaya yaz
                dosyayaz.Write("Ad� Soyad�:" + Adsoyad + ayrac + "E-Posta Adresi:" + Eposta + ayrac +
                    "Tahsili:" + Tahsili + ayrac + "Verilen Oy:" + Oy + ayrac + "Mesaj:" + Mesaj + ayrac +
                    "Ip Adresi:" + ipadresi + ayrac + "��lem Tarihi:" + DateTime.Now.ToString() + ayrac);
                dosyayaz.Close();//Dosyay� kapat
            }
            catch (Exception hata)
            {
                ViewData["Durummesaji"] = "Hata Olu�tu. L�tfen tekrar deneyiniz. Hata:" + hata.Message;
                return;
            }
            ViewData["Durummesaji"] = "Bilgileriniz i�in te�ekk�rler.";
        }
    }
}
