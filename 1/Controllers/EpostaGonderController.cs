using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System.Threading.Tasks;
using System.Configuration;

/*
 * Mailkit adlı bir .Net kitaplığı kullanılmıştır.
 * Nuget paket yöneticisi ile "Mailkit" aranır ve yüklenir.
 * Veya .NET Core komut satırından şöyle yüklenebilir: dotnet add package MailKit
 * Aşağıdaki kitaplıklar, dahil edilmelidir.
 * using MailKit.Net.Smtp;
   using MimeKit;
 * */
namespace BenimsiteMvc.Controllers
{
    public class EpostaGonderController : Controller
    {
        private readonly IConfiguration _configuration;

        public EpostaGonderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Epostagonder")]
        public async Task<IActionResult> Index(string Adsoyad, string Eposta, string Mesaj)
        {
            //Mail içeriği bilgilerini tutan MimeMessage hazırla
            MimeMessage mailmesaji = new MimeMessage();
            //Mesajı Gönderene ait bilgiler
            mailmesaji.From.Add(new MailboxAddress("Kocaeli Myo Bilgisayar Programcılığı Web Sitesi", "kmyoweb@hotmail.com"));
            mailmesaji.ReplyTo.Add(new MailboxAddress(Adsoyad, Eposta)); // Cevap gelirse kullanıcıya gider
            //Alıcıya ait bilgiler
            MailboxAddress kime = new MailboxAddress("Site Yöneticisi", "mustafaof@gmail.com");
            MailboxAddress kime2 = new MailboxAddress("Site Yöneticisi", "mustafaof@kocaeli.edu.tr");
            mailmesaji.To.Add(kime);
            mailmesaji.To.Add(kime2); 
            mailmesaji.Subject = "Web Sitesinden Gönderilen Mesaj";//Mesaj konusu
            //Mesajın gövdesi            
            BodyBuilder mesajgovdesi = new BodyBuilder();
            mesajgovdesi.HtmlBody = "<h1>" + Mesaj + "</h1><br>Gönderme Tarihi:" + DateTime.Now.ToString();//Mesaj gövdesinde Html kodları kullanılabilir.
            mesajgovdesi.TextBody = Mesaj;//Bazı E-Posta okuyucaları sadece düz metin okuyabilirler
            //Eklenecek dosyanın tam yolu gerekir.
            //Microsoft.AspNetCore.Hosting.IWebHostEnvironment sunucuortami = null;
            //mesajgovdesi.Attachments.Add(sunucuortami.WebRootPath + "\\dosyaismi.pdf");//Mail içeriğine dosya eklenebilir.
            mailmesaji.Body = mesajgovdesi.ToMessageBody();
            //Mail gönderimini yapacak olan E-Posta hesabına ait bilgiler. 
            //Microsoft Hotmail veya Google Gmail hesapları, gönderici olarak kullanılabilir. 
            //Ancak iki faktörlü doğrulama açıksa göndermez. Bunun yerine uygulama şifresi oluşturulmalıdır.
            //Gmail için: Güvenlik>Uygulama şifreleri oluşturulmalıdır. 2FA açık olmalı ve oluşan şifre, appsettings.json daki şifre alanına girilmelidir.
            //Fakat belirli kısıtlamaları vardır. Örn. Gönderme garantisi yoktur.
            //Önerilen, bir sitenin kendisine ait olan Smtp sunucu ve hesap bilgilerinin kullanımıdır.
            SmtpClient mailgonderici = new SmtpClient();
            //E-Posta gönderici sunucu adı, Gönderim (Smtp) portu, Gönderim güvenliği Tls:587, Ssl:465 ise Ssl gönderimi yapılacaktır.
            var epostasunucu = _configuration["EmailSettings:Host"];//Epostayı göndermek için sunucu adı. appsettings.json dosyasında yazılıdır.
            var epostakullanici = _configuration["EmailSettings:Username"];//Kullanıcı adı
            var epostasifre = _configuration["EmailSettings:Password"];//Şifre. 
            await mailgonderici.ConnectAsync(epostasunucu, 587, SecureSocketOptions.StartTls);
            await mailgonderici.AuthenticateAsync(epostakullanici, epostasifre);
            //Epostayı göndermek için gerekli olan gönderici kullanıcı E-Posta hesap bilgileri.
            //Bu bilgiler, appsettings.json dosyasındadır.
            try
            {
                await mailgonderici.SendAsync(mailmesaji);//Mail mesajını gönder                
            }
            catch (Exception hata)
            {
                //Eğer hata oluşursa
                ViewData["sonucmesaj"] = "<strong style='color:#f00;'>Gönderim sırasında hata oluştu. Hata:" + hata.Message + "</strong>";
                return View();//Görünümü aç
            }
            await mailgonderici.DisconnectAsync(true);//Bağlantıyı kes
            mailgonderici.Dispose();//Bellekten at
            ViewData["sonucmesaj"] = "<strong>Mesajınız gönderilmiştir.</strong>";

            return View();
        }
    }
}
