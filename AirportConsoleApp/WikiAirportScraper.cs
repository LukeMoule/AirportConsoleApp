using HtmlAgilityPack;

namespace AirportConsoleApp
{
    internal class WikiAirportScraper
    {
        private string url;

        public WikiAirportScraper(string inUrl)
        {
            url = inUrl;
        }

        public bool Scrape(string xpath)
        {
            try
            {
                var web = new HtmlWeb();

                // downloading to the target page
                // and parsing its HTML content
                var document = web.Load(url);
                var nodes = document.DocumentNode.SelectNodes(xpath);

                foreach (var node in nodes)
                {
                    var childNodes = node.SelectNodes("*");
                    foreach (var child in childNodes)
                    {
                        Console.Write("|");
                        Console.Write(HtmlEntity.DeEntitize(child.InnerText).Trim());
                    }
                    Console.WriteLine();
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Bad URL(s)");
                return false;
            }

        }
    }
}
