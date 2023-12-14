using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace WebShop.Pages
{
    public class IndexModel : PageModel
    {
        public List<Artikel> LstArtikel = null;
        public void OnGet()
        {
            LstArtikel = Artikel.AlleLesen();
            LstArtikel.Reverse();
            LstArtikel = LstArtikel.Take(LstArtikel.Count > 10 ? 10 : LstArtikel.Count).ToList();
        }
    }
}