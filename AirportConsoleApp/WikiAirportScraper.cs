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
                    Iata = ParseNode(cols[0]),
                    Icao = ParseNode(cols[1]),
                    AirportName = ParseNode(cols[2]),
                    LocationServed = ParseNode(cols[3]),
                    Time = ParseNode(cols[4]),
                    Dst = ParseNode(cols[5])
                };
                airports.Add(airport);
                Console.WriteLine(airport);
            }
            return airports;
        }

        private string ParseNode(HtmlNode node)
        {
            return HtmlEntity.DeEntitize(node.InnerText).Trim();
        }
    }
}
