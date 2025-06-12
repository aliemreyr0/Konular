using System.ComponentModel.DataAnnotations;

namespace BenimsiteMvc.Models
{
    //KampanyaVt2, görünümündeki (View) form alanlarında veri giriş kontrolü yapan model
    //İlgili görünüm dosyasının ilk deyimi şu şekilde olmalıdır. @model BenimsiteMvc.Models.KampanyaVt2GirisModel
    //Mesajların gözükmesi için View kısmında gerekli span'ların olması şarttır.
    //Örnek <span asp-validation-for="Adsoyad" class="text-danger"></span>
    public class KampanyaVt2GirisModel
    {
        [Required(ErrorMessage = "Adı Soyadı alanı zorunludur.")]
        [MaxLength(25, ErrorMessage = "Adı Soyadı en fazla 25 karakter olmalıdır.")]
        public string Adsoyad { get; set; }

        [Required(ErrorMessage = "Vatandaşlık Numarası alanı zorunludur.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "T.C. Numarası sadece rakamlardan oluşmalıdır.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C. Numarası 11 hane olmalıdır.")]
        public string Tcno { get; set; }

        [Required(ErrorMessage = "E-Posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        [MaxLength(40, ErrorMessage = "E-posta en fazla 40 karakter olmalıdır.")]
        public string Eposta { get; set; }

        [Required(ErrorMessage = "Bir kampanya seçmelisiniz.")]
        public int? Kampanyano { get; set; }
    }
}
