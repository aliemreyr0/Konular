using System.ComponentModel.DataAnnotations;

namespace BenimsiteMvc.Models
{
    public class CokluDosyaModel
    {
        // Dosya yükleme için gerekli özellikler
        [Required(ErrorMessage = "Lütfen en az bir dosya seçiniz")]
        [Display(Name = "Dosyalar")]
        public List<IFormFile> Dosyalar { get; set; } //Birden fazla dosya için List kullanıldı

        // Yükleme sonrası bilgilendirme mesajları için kullanılacaktır.
        public string? Mesaj { get; set; }
        public bool BasariliMi { get; set; }
        public List<string> YuklenenDosyalar { get; set; } = new List<string>();
    }
}
