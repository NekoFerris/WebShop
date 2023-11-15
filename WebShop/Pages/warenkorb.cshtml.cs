using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Primitives;
using System.Collections.Specialized;

namespace WebShop.Pages
{
    public class warenkorbModel : PageModel
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
                Bestellung bestellung = new();
                ObservableCollection<BestellPos> bestellPos = new();
                _bestellung = bestellung.Laden(Int32.Parse(HttpContext.Session.GetString("id")));
                _bestellung.LstBestPoss = BestellPos.AlleLaden(Int32.Parse(HttpContext.Session.GetString("id")));
                List<KeyValuePair<string, StringValues>> daten = Request.Query.Where(q => q.Key.Split(",")[0] == "menge").ToList(); ;
                foreach (KeyValuePair<string, StringValues> valuePair in daten)
                {
                    try
                    {
                        int id = Int32.Parse(valuePair.Key.Split(",")[1]);
                        int mengeneu = Int32.Parse(valuePair.Value.FirstOrDefault());
                        int mengealt = bestellung.LstBestPoss.Where(b => b.Id == id).Single().Anzahl;
                        if (mengeneu != mengealt && mengeneu > 0)
                        {
                            bestellung.LstBestPoss.Where(b => b.Id == id).Single().Anzahl = mengeneu;
                        }
                        else if(mengeneu == 0)
                        {
                            bestellung.LstBestPoss.Remove(bestellung.LstBestPoss.Where(b => b.Id == id).Single());
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
            Bestellung bestellung = new();
            ObservableCollection<BestellPos> bestellPos = new();
            _bestellung = bestellung.Laden(Int32.Parse(HttpContext.Session.GetString("id")));
            _bestellung.LstBestPoss = BestellPos.AlleLaden(Int32.Parse(HttpContext.Session.GetString("id")));
            foreach (BestellPos pos in _bestellung.LstBestPoss)
            {
                artikels.Add(Artikel.Lesen(pos.ArtikelId));
            }
        }
    }
}
