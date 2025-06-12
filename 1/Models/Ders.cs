using System.ComponentModel.DataAnnotations;

namespace BenimsiteMvc.Models
{
    public class Ders
    {
        [Display(Name = "Kimlik")]        
        public int Id { get; set; }
        [Display(Name = "Dersin Adı")]
        [Required(ErrorMessage = "Ders adı boş bırakılamaz.")]
        public string DersAdi { get; set; }
        [Display(Name = "Öğretim Elemanı")]
        [Required(ErrorMessage = "Öğretim Elemanı boş bırakılamaz.")]
        public string Hoca { get; set; }
        [Display(Name = "Kredi")]
        [Required(ErrorMessage = "Kredi alanı boş bırakılamaz.")]
        public int Kredi { get; set; }
        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz.")]
        public string Aciklama { get; set; }
    }
}
