using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace WebShop.Pages
{
    public class WarenkorbModel : PageModel
    {
        public Bestellung Bestellung = new();

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lhash")))
            {
                TabelleFuellen();
            }
            else
            {
                Response.Redirect("/");
            }
        }

        public void OnPostAktuallesieren()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lhash")))
            {
                Bestellung = Bestellung.OffeneBestellung(Int32.Parse(HttpContext.Session.GetString("id")));
                List<KeyValuePair<string, StringValues>> daten = HttpContext.Request.Form.Where(q => q.Key.Split(",")[0] == "menge").ToList();
                foreach (KeyValuePair<string, StringValues> valuePair in daten)
                {
                    try
                    {
                        int pos = Int32.Parse(valuePair.Key.Split(",")[1]);
                        int mengeneu = Int32.Parse(valuePair.Value);
                        BestellPos bestellPos = Bestellung.LstBestPoss.Where(b => b.Id == pos).Single();
                        int mengealt = bestellPos.Menge;
                        if (mengeneu != mengealt && mengeneu > 0)
                        {
                            bestellPos.MengeAendern(mengeneu);
                        }
                        else if (mengeneu == 0)
                        {
                            Bestellung.ArtikelEntfernen(Bestellung.LstBestPoss.IndexOf(bestellPos));
                        }
                    }
                    catch
                    {
                    }
                }
                TabelleFuellen();
            }
            else
            {
                Response.Redirect("/");
            }
        }

        public void OnPostBestellen()
        {
            Bestellung best = Bestellung.OffeneBestellung(Int32.Parse(HttpContext.Session.GetString("id")));
            best.Bestellen();
        }

        public void TabelleFuellen()
        {
            Bestellung = Bestellung.OffeneBestellung(Int32.Parse(HttpContext.Session.GetString("id")));
        }
    }
}