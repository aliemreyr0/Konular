using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BenimsiteMvc.Models
{
    public class DosyaModel
    {
        //Dosyaukleme1 adlı denetleyicinin model dosyası
        [Required(ErrorMessage ="Lütfen bir dosya seçiniz")]
        [Display(Name="Bir Resim Dosyası Seçiniz")]
        public IFormFile Dosya { get; set; }
    }
}
