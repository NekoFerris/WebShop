using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages.Shared
{
    public class loginModel : PageModel
    {
        public const string LoginHash = "lhash";

        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string passwort { get; set; }

        public void OnGet()
        {
        }

        public void OnPostLogin()
        {
            if (Kunde.Auth(email, passwort, out Kunde k))
            {
                HttpContext.Session.SetString("lhash", k.GetHashCode().ToString());
                HttpContext.Session.SetString("id", k.Id.ToString());
                Response.Redirect("/");
            }
        }
    }
}