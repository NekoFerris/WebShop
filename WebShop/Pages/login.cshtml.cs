using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages.Shared
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            Response.Redirect("/");
        }
        public void OnPostLogin(string email, string passwort)
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("lhash")))
            {
                if (string.IsNullOrWhiteSpace(passwort) || string.IsNullOrWhiteSpace(email))
                    return;
                if (Kunde.Auth(email.Trim(), passwort, out Kunde k))
                {
                    HttpContext.Session.SetString("lhash", k.GetHashCode().ToString());
                    HttpContext.Session.SetString("id", k.Id.ToString());
                    Response.Redirect(Request.Headers["Referer"].ToString());
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }
    }
}