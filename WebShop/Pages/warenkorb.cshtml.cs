using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopBase.Persistenz;
using System.Collections.ObjectModel;

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
                Bestellung bestellung = new();
                ObservableCollection <BestellPos> bestellPos = new();
                _bestellung = bestellung.Laden(Int32.Parse(HttpContext.Session.GetString("id")));
                _bestellung.LstBestPoss = BestellPos.AlleLaden(Int32.Parse(HttpContext.Session.GetString("id")));
                foreach(BestellPos pos in _bestellung.LstBestPoss)
                {
                    artikels.Add(Artikel.Lesen(pos.ArtikelId));
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }
    }
}
