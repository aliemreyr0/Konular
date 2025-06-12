using System.ComponentModel.DataAnnotations;

namespace BenimsiteMvc.Models
{
    public class UrunlerModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        public string Urunadi { get; set; }

        [Required(ErrorMessage = "Kategori alanı boş bırakılamaz.")]
        public string Kategori { get; set; }

        [Required(ErrorMessage = "Fiyat girilmelidir.")]
        [RegularExpression(@"^\d+([.,]\d{1,2})?$", ErrorMessage = "Fiyat alanına yalnızca sayısal bir değer giriniz.")]
        [Range(0.01, 1000000, ErrorMessage = "Fiyat geçerli bir değer olmalıdır.")]
        public decimal? Fiyat { get; set; } // nullable yapılırsa boş geçilince düzgün mesaj gösterilir
    }
}
