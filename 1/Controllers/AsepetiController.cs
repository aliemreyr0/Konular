using Microsoft.AspNetCore.Mvc;

namespace BenimsiteMvc.Controllers
{
    public class AsepetiController : Controller
    {
        public IActionResult Index()
        {
            //Sepetteki ürünler
            var sepet = new List<string> { "Defter", "Kitap", "Silgi", "Kalem", "Kalemtraş", "Klavye" };
            sepet.Add("Mouse");//Yeni bir ürün daha eklendi
            sepet.Add("Çöp Kutusu");

            // Virgül ile birleştirip Session'a yazalım
            var sepetMetni = string.Join(",", sepet);
            HttpContext.Session.SetString("sepet", sepetMetni);//Oturum değişkeninin adı "sepet"
            ViewData["Mesaj"] = "Alış Veriş Sepeti oluşturuldu.";
            return View("Index");
        }

        // Sepeti görüntüler (Session'dan okur)
        public IActionResult SepetiGoster()
        {
            //Oturum değişkenin oku
            var sepetMetni = HttpContext.Session.GetString("sepet");

            List<string> sepet2 = new();//Geçici bir list

            if (!string.IsNullOrEmpty(sepetMetni))
            {
                sepet2 = sepetMetni.Split(',').ToList();//Virgül gördüğün yerden parçalara ayır ve List'e çevir
            }
            return View(sepet2);//sepet2 adlı list'i view katmanına gönder. View'in en üstüne @model List<string> ifadesi eklenmelidir.
        }

        // Sepeti temizler
        public IActionResult SepetiTemizle()
        {
            //sepet adlı oturum değişkeni silinir
            HttpContext.Session.Remove("sepet");//sepet adlı oturum değişkenini siler

            ViewData["Mesaj"] = "Sepet temizlendi.";
            return View("Index");
        }
    }
}
