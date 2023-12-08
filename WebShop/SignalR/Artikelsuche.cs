using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Nodes;

namespace WebShop.SignalR
{
    public class Artikelsuche : Hub
    {
        public void ArtikelSuche(string suchb)
        {
            JsonArray jsonArray = new();
            JsonNode jsonNode;
            if (!suchb.IsNullOrEmpty())
            {
                List<Artikel> _lstartikels = Artikel.AlleLesen(suchb);
                List<double> _lstGleichheit = new();
                foreach (Artikel a in _lstartikels)
                {
                    _lstGleichheit.Add(StringHelfer.CompareStrings(a.Bezeichnung, suchb));
                }
                for (int i = 0; i < (_lstGleichheit.Count > 5 ? 5 : _lstGleichheit.Count); i++)
                {
                    jsonNode = _lstartikels[i].Bezeichnung;
                    jsonArray.Add(jsonNode);
                }
                SendMessage(jsonArray).Wait();
            }
            else
            {
                SendMessage(jsonArray).Wait();
            }
        }

        public async Task SendMessage(JsonArray jsonArray)
        {
            await Clients.All.SendAsync("GefundeneArtikel", jsonArray);
        }
    }
}