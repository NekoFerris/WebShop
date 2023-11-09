using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages.Shared
{
    public class loginModel : PageModel
    {
        public void OnPostLogin(string email, string passwort)
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