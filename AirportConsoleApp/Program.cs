
namespace AirportConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create URLs
            const string BASE_URL = "https://en.wikipedia.org/wiki/List_of_airports_by_IATA_airport_code:_";
            const int ASCII_CODE_A = 65;
            const int LETTERS_IN_ALPHABET = 1;

            string[] urls = new string[LETTERS_IN_ALPHABET];

            for (int i= 0; i<urls.Length; i++)
            {
                char c = Convert.ToChar(i+ASCII_CODE_A);
                urls[i] = BASE_URL + c;
            }

            //scrape the URLs
            foreach(string url in urls)
            {
                var scraper = new WikiAirportScraper(url);

                // Using XPath to select all table rows except those with the class "sortbottom"
                if (scraper.Scrape("//*[@id=\"mw-content-text\"]/div[1]/table//tr[position()>0 and not(contains(@class, \"sortbottom\"))]"))
                {
                    Console.WriteLine("success");
                }
            }
            
        }
    }
}