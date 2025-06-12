using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace BenimsiteMvc.Controllers
{
    public class VerigirController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string Adsoyad, string Eposta)
        {
            //Form'dan gelen veriler, parametre olarak alınır. [HttpPost] direktifi kullanılır.
            //HtmlEncode ile kullanıcının metin kutularına zararlı kod girmesi engellenir.
            string ipadresi = HttpContext.Connection.RemoteIpAddress.ToString();
            ViewData["sonucmesaj"] = "<strong style='color:blue;'>Adı Soyadı:</strong> " + HttpUtility.HtmlEncode(Adsoyad) + "<br>" +
                "<strong style='color:blue;'>E-Posta Adresi: </strong>" + HttpUtility.HtmlEncode(Eposta) + "<br>" +
                "<strong style='color:blue;'>Ip Adresiniz: </strong>" + ipadresi;

            return View();
        }

        public IActionResult Bilgigir()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Bilgigir(string Adsoyad, string Eposta, string Tahsili)
        {
            //Bu metotun görünümünü açmak için adres satırında alan isminden sonra /Verigir/Bilgigir kullanılmalıdır.
            //HtmlEncode ile kullanıcının metin kutularına zararlı kod girmesi engellenir.
            string ipadresi = HttpContext.Connection.RemoteIpAddress.ToString();
            ViewData["sonucmesaj"] = "<strong style='color:blue;'>Adı Soyadı:</strong> " + HttpUtility.HtmlEncode(Adsoyad) + "<br>" +
                "<strong style='color:blue;'>E-Posta Adresi: </strong>" + HttpUtility.HtmlEncode(Eposta) + "<br>" +
                "<strong style='color:blue;'>Tahsili: </strong>" + HttpUtility.HtmlEncode(Tahsili) + "<br>" +
                "<strong style='color:blue;'>Ip Adresiniz: </strong>" + ipadresi;
            return View();
        }
    }
}
