using Benimsite2.Servisler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class IletisimModel : PageModel
    {
        //Baðýmlý enjeksiyon için servisi tanýmý deðiþkeni oluþturulur.
        private readonly IServis1 _servis1;// IServis1 tipinde bir deðiþken tanýmlanýr.
        public string Sonucyazi { get; set; }// Sayfada gösterilecek olan sonucu tutacak bir property.

        //Model i oluþturan ana metoda (Oluþturucu metot) parametre olarak IServis1 verilir.
        public IletisimModel(IServis1 servisparametresi)
        {
            _servis1 = servisparametresi;// Baðýmlýlýk enjeksiyonu sayesinde servis parametresi (_servis1) alýnýr ve _servis1 deðiþkenine atanýr.
        }
        public void OnGet()
        {
            //Sayfa ilk yüklendiðinde çalýþan olay
            Sonucyazi = _servis1.Merhaba();  // Servisin Merhaba metodunu çaðýrarak Sonucyazi deðiþkenine deðer atanýr.
        }
    }
}
