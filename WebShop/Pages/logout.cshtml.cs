using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages.Shared
{
    public class logoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/");
        }
    }
}