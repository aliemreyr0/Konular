using System.ComponentModel.DataAnnotations;

namespace BenimsiteMvc.Models
{
    public class OgrenciModel
    {
        public int Id { get; set; }//Otomatik atanacak

        [Required(ErrorMessage = "Öğrenci numarası, zorunlu bir alandır.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Öğrenci numarası 9 haneli olmalı ve sadece rakamlardan oluşmalıdır.")]
        public string Ogrencino { get; set; }

        [Required(ErrorMessage = "Adı Soyadı, zorunlu bir alandır.")]
        public string Adsoyad { get; set; }

        [Required(ErrorMessage = "Bölüm, zorunlu bir alandır.")]
        public string Bolum { get; set; }
        [Required(ErrorMessage = "Kayıt Tarihi, zorunlu bir alandır.")]
        [DataType(DataType.DateTime, ErrorMessage = "Kayıt Tarihi için geçerli bir tarih giriniz.")]        
        public DateTime Kayittarihi { get; set; }//Otomatik atanacak
    }
}
