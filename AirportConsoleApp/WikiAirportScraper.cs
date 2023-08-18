using HtmlAgilityPack;
using System.Security.Cryptography;

namespace AirportConsoleApp
{
    internal class WikiAirportScraper
    {
        private string _url;

        public WikiAirportScraper(string url)
        {
            _url = url;
        }

        public List<Airport> Scrape(string xpath)
        {
            List<Airport> airports = new List<Airport>();

            var web = new HtmlWeb();
            var document = web.Load(_url);

            var rows = document.DocumentNode.SelectNodes(xpath);
            foreach (var row in rows)
            {

                var cols = row.SelectNodes("*");

                var airport = new Airport
                {
                    IATA = ParseNode(cols[0]),
                    ICAO = ParseNode(cols[1]),
                    AirportName = ParseNode(cols[2]),
                    LocationServed = ParseNode(cols[3])
                };
                airports.Add(airport);
            }
            return airports;
        }

        private string ParseNode(HtmlNode node)
        {
            return HtmlEntity.DeEntitize(node.InnerText).Trim();
        }
    }
}
