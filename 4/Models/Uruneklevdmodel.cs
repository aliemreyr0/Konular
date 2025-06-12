using System.ComponentModel.DataAnnotations;

namespace BenimsiteMvc.Models
{
    public class Uruneklevdmodel
    {
        //Ürün ekleme formunda kullanılan veri doğrulama modeli
        [Required(ErrorMessage = "Ürün adı boş bırakılamaz.")]
        [StringLength(25, ErrorMessage = "Ürün adı en fazla 25 karakter olabilir.")]
        public string Urunadi { get; set; }

        [Required(ErrorMessage = "Fiyat alanı boş bırakılamaz.")]
        [RegularExpression(@"^\d{1,6}([.,]\d{1,2})?$", ErrorMessage = "Geçerli bir fiyat giriniz. Örnek: 123456 veya 1234,56")]
        public string Fiyat { get; set; }
    }
}
