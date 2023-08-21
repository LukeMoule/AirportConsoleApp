using HtmlAgilityPack;
using System.Security.Cryptography;

namespace AirportConsoleApp
{
    internal class WikiAirportScraper
    {
        private string[] _urls;
        private string _xpath;

        public WikiAirportScraper(string[] urls, string xpath)
        {
            _urls = urls;
            _xpath = xpath;
        }

        public List<Airport> Scrape()
        {
            List<Airport> output = new List<Airport>();

            foreach (string url in _urls)
            {
                List<Airport> airportList = ScrapeOne(url);
                output.AddRange(airportList);
            }
            return output;
        }

        private List<Airport> ScrapeOne(string url)
        {
            List<Airport> airports = new List<Airport>();

            var web = new HtmlWeb();
            var document = web.Load(url);

            var rows = document.DocumentNode.SelectNodes(_xpath);
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
