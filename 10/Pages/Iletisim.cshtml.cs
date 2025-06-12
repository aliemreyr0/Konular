using Benimsite2.Servisler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Benimsite2.Pages
{
    public class IletisimModel : PageModel
    {
        private readonly IMyService _myService;
        public string Sonucyazi { get; set; }
        public IletisimModel(IMyService myService)
        {
            _myService = myService;
        }
        public void OnGet()
        {
            Sonucyazi = _myService.Merhaba();
        }
    }
}
