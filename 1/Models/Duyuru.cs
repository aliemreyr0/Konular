using System.ComponentModel.DataAnnotations;

namespace BenimsiteMvc.Models
{
    public class Duyuru
    {
        [Display(Name = "Kimlik")]
        public int Id { get; set; }
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }
        [Display(Name = "İçerik")]
        public string Icerik { get; set; }
        [Display(Name = "Aktif:1, Pasif:0")]
        public int Aktif { get; set; }

        [Display(Name = "Yayım Tarihi")]
        public DateTime YayimTarihi { get; set; }        
    }
}
