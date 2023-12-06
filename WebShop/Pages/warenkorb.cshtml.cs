using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Primitives;
using System.Collections.Specialized;
using System;

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
                        int mengealt = Bestellung.LstBestPoss.Where(b => b.Id == pos).Single().Menge;
                        if (mengeneu != mengealt && mengeneu > 0)
                        {
                            throw new NotImplementedException();
                        }
                        else if (mengeneu == 0)
                        {
                            throw new NotImplementedException();
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
            Bestellung = Bestellung.OffeneBestellung(Int32.Parse(HttpContext.Session.GetString("id")));
        }
    }
}