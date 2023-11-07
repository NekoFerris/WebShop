using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages
{
    public class regModel : PageModel
    {
        public const string LoginHash = "lhash";
        public const string LoginAge = "lage";

        [BindProperty]
        public Kunde k { get; set; } = new();

        public void OnGet()
        {
        }

        public void OnPostReg()
        {
            k.Anlegen();
            k = new();
            Response.Redirect("/");
        }
    }
}