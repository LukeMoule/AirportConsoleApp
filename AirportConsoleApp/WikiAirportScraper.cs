using HtmlAgilityPack;

namespace AirportConsoleApp
{
    internal class WikiAirportScraper
    {
        private string url;
        private string[]? headerArray;
        private string[,]? rows;

        public WikiAirportScraper(string inUrl)
        {
            url = inUrl;
        }

        public bool Scrape(string headerXpath, string rowsXpath)
        {
            try
            {
                var web = new HtmlWeb();

                // downloading to the target page
                // and parsing its HTML content
                var document = web.Load(url);
                var header = document.DocumentNode.SelectSingleNode(headerXpath);
                var headings = header.SelectNodes("*");
                headerArray = new string[headings.Count];
                Console.WriteLine(headerArray.Length);
                foreach (var heading in headings)
                {
                    Console.Write("|");
                    Console.Write(HtmlEntity.DeEntitize(heading.InnerText).Trim());
                }
                Console.WriteLine();


                var nodes = document.DocumentNode
                    .SelectNodes(rowsXpath);

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
