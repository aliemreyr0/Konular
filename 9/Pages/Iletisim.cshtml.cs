using Benimsite2.Servisler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class IletisimModel : PageModel
    {
        //Ba��ml� enjeksiyon i�in servisi tan�m� de�i�keni olu�turulur.
        private readonly IServis1 _servis1;// IServis1 tipinde bir de�i�ken tan�mlan�r.
        public string Sonucyazi { get; set; }// Sayfada g�sterilecek olan sonucu tutacak bir property.

        //Model i olu�turan ana metoda (Olu�turucu metot) parametre olarak IServis1 verilir.
        public IletisimModel(IServis1 servisparametresi)
        {
            _servis1 = servisparametresi;// Ba��ml�l�k enjeksiyonu sayesinde servis parametresi (_servis1) al�n�r ve _servis1 de�i�kenine atan�r.
        }
        public void OnGet()
        {
            //Sayfa ilk y�klendi�inde �al��an olay
            Sonucyazi = _servis1.Merhaba();  // Servisin Merhaba metodunu �a��rarak Sonucyazi de�i�kenine de�er atan�r.
        }
    }
}
