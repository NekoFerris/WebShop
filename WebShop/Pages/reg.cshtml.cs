using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages
{
    public class regModel : PageModel
    {
        public Kunde Kunde { get; set; } = new();

        public void OnGet()
        {
        }

        public void OnPostReg()
        {
            Kunde.Anlegen();
        }
    }
}