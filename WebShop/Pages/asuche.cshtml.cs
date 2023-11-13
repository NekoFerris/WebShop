using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Nodes;

namespace WebShop.Pages.Shared
{
    public class asucheModel : PageModel
    {
        public List<Artikel> artikels = new();
        public string korrecktur = "";

        public void OnGet()
        {
            artikels = Artikel.AlleLesen();
        }

        public void OnPostSuchen(string suchb)
        {
            if (suchb != null)
            {
                artikels = Artikel.AlleLesen(suchb);
                if (artikels.IsNullOrEmpty())
                {
                    List<Artikel> _lstArt = new();
                    List<double> _lstGleichheit = new();
                    _lstArt = Artikel.AlleLesen();
                    foreach (Artikel a in _lstArt)
                    {
                        _lstGleichheit.Add(StringHelfer.CompareStrings(a.Bezeichnung, suchb));
                    }
                    korrecktur = _lstGleichheit.Max() > 0 ? _lstArt[_lstGleichheit.IndexOf(_lstGleichheit.Max())].Bezeichnung.ToString() : "";
                }
            }
            else
            {
                artikels = Artikel.AlleLesen();
            }
        }

        public void OnPostAddArtikel(int artnr, int menge)
        {
            Bestellung.ArtikelHinzufuegen(Int32.Parse(HttpContext.Session.GetString("id")), artnr, menge);
            Response.Redirect("/");
        }
    }
}