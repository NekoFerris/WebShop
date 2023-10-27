using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopBase;

namespace WebShop.Pages.Shared
{
    [BindProperties(SupportsGet = true)]
    public class asucheModel : PageModel
    {
        public string suchb { get; set; }

        public List<Artikel> artikels = new();

        public void OnGet()
        {
            artikels = Artikel.AlleLesen();
        }

        public void OnPostSuchen()
        {
            if (suchb != null)
                artikels = Artikel.AlleLesen(suchb);
            else
                artikels = Artikel.AlleLesen();
        }
    }
}
