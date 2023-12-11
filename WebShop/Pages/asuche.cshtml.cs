using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages.Shared
{
    public class AsucheModel : PageModel
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
                if (artikels.Count == 0)
                {
                    List<Artikel> _lstArt;
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

        public void OnPostAddArtikel(int? artnr, int? menge)
        {
            if (artnr == null || menge == null || HttpContext.Session.GetString("id") == null)
                return;
            Bestellung.ArtikelHinzufuegen(Int32.Parse(HttpContext.Session.GetString("id")), artnr.Value, menge.Value);
            Response.Redirect("warenkorb");
        }
    }
}