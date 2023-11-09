using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages.Shared
{
    public class asucheModel : PageModel
    {
        public List<Artikel> artikels = new();

        public void OnGet()
        {
            artikels = Artikel.AlleLesen();
        }

        public void OnPostSuchen(string suchb)
        {
            if (suchb != null)
                artikels = Artikel.AlleLesen(suchb);
            else
                artikels = Artikel.AlleLesen();
        }

        public void OnPostAddArtikel(int artnr, int menge)
        {
            Bestellung.ArtikelHinzufuegen(Int32.Parse(HttpContext.Session.GetString("id")), artnr, menge);
            Response.Redirect("/");
        }
    }
}