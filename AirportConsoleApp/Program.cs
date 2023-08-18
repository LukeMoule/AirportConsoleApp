/*
TODO: improve column assignment
TODO: excel character encoding

Issues with wiki data scraping:
Two columns randomly combined into one / columns missing
includes random extras such as superscript links in the data field: see VELP, PAAK
coordinate data requires an extra HTML request
 */
using CsvHelper;
using System.Globalization;

namespace AirportConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create URLs
            const string BASE_URL = "https://en.wikipedia.org/wiki/List_of_airports_by_IATA_airport_code:_";
            const int ASCII_CODE_A = 65;
            const int LETTERS_IN_ALPHABET = 26;

            for (int i= 0; i<LETTERS_IN_ALPHABET; i++)
            {
                char c = Convert.ToChar(i+ASCII_CODE_A);
                string url = BASE_URL + c;
                var scraper = new WikiAirportScraper(url);

                // Using XPath to select all table rows except those with the class "sortbottom"
                // position()>1 ignores header row
                var records = scraper.Scrape("//*[@id=\"mw-content-text\"]/div[1]/table//tr[position()>1 and not(contains(@class, \"sortbottom\"))]");

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string csvFolder = Path.Combine(baseDirectory, "data", "csv");
                if (!Directory.Exists(csvFolder))
                {
                    Directory.CreateDirectory(csvFolder);
                }
                string csvFile = Path.Combine(csvFolder, $"airports_{c}.csv");
                
                using (var writer = new StreamWriter(csvFile))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
            
        }
    }
}