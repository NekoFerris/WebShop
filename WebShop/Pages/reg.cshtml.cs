using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages
{
    public class RegModel : PageModel
    {
        [BindProperty]
        public Kunde K { get; set; } = new();

        public void OnGet()
        {
        }

        public void OnPostReg()
        {
            K.Pw = ShopBase.Persistenz.DBTools.HashPassword(K.Pw);
            K.Anlegen();
            K = new();
            Response.Redirect("/");
        }
    }
}