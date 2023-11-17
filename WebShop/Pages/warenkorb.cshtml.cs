using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Primitives;
using System.Collections.Specialized;
using System;

namespace WebShop.Pages
{
    public class WarenkorbModel : PageModel
    {
        public Bestellung _bestellung = new();
        public List<Artikel> artikels = new();

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
                ObservableCollection<BestellPos> bestellPos = new();
                _bestellung = Bestellung.Laden(Int32.Parse(HttpContext.Session.GetString("id")));
                _bestellung.LstBestPoss = BestellPos.AlleLaden(_bestellung.Id);
                List<KeyValuePair<string, StringValues>> daten = HttpContext.Request.Form.Where(q => q.Key.Split(",")[0] == "menge").ToList();
                foreach (KeyValuePair<string, StringValues> valuePair in daten)
                {
                    try
                    {
                        int pos = Int32.Parse(valuePair.Key.Split(",")[1]);
                        int mengeneu = Int32.Parse(valuePair.Value);
                        int mengealt = _bestellung.LstBestPoss.Where(b => b.Id == pos).Single().Anzahl;
                        if (mengeneu != mengealt && mengeneu > 0)
                        {
                            _bestellung.LstBestPoss.Where(b => b.Id == pos).Single().MengeAendern(mengeneu);
                        }
                        else if (mengeneu == 0)
                        {
                            Bestellung.ArtikelEntfernen(Int32.Parse(HttpContext.Session.GetString("id")), pos);
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

        public void TabelleFuellen()
        {
            _bestellung = Bestellung.Laden(Int32.Parse(HttpContext.Session.GetString("id")));
            _bestellung.LstBestPoss = BestellPos.AlleLaden(_bestellung.Id);
            foreach (BestellPos pos in _bestellung.LstBestPoss)
            {
                artikels.Add(Artikel.Lesen(pos.ArtikelId));
            }
        }
    }
}