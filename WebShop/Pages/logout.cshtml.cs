using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages.Shared
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/");
        }

        public void OnPost()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/");
        }
    }
}