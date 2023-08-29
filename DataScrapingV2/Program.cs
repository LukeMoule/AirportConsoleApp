using System.Formats.Asn1;

namespace DataScrapingV2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string BASE_URL = "https://en.wikipedia.org/wiki/List_of_airports_by_IATA_airport_code:_";
            const int ASCII_CODE_A = 65;
            const int LETTERS_IN_ALPHABET = 1;

            string[] urls = new string[LETTERS_IN_ALPHABET];
            for (int i = 0; i < LETTERS_IN_ALPHABET; i++)
            {
                char c = Convert.ToChar(i + ASCII_CODE_A);
                string url = BASE_URL + c;
                urls[i] = url;
            }
            var scraper = new WikiAirportScraper(urls);
            List<Airport> airports = await scraper.Scrape();
        }
    }
}