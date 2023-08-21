/*
TODO: improve column assignment
TODO: excel character encoding

Issues with wiki data scraping:
Two columns randomly combined into one / columns missing
includes random extras such as superscript links in the data field: see VELP, PAAK
coordinate data requires an extra HTML request
 */


using System;

namespace AirportConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Using XPath to select all table rows except those with the class "sortbottom"
            // position()>1 ignores header row
            const string xpath = "//*[@id=\"mw-content-text\"]/div[1]/table//tr[position()>1 and not(contains(@class, \"sortbottom\"))]";
            // create URLs
            const string BASE_URL = "https://en.wikipedia.org/wiki/List_of_airports_by_IATA_airport_code:_";
            const int ASCII_CODE_A = 65;
            const int LETTERS_IN_ALPHABET = 26;
            
            // Write a file for each letter of the alphabet
            /*
            for (int i= 0; i<LETTERS_IN_ALPHABET; i++)
            {
                char c = Convert.ToChar(i+ASCII_CODE_A);
                string[] url = { BASE_URL + c };
                var scraper = new WikiAirportScraper(url, xpath);
                var airportList = scraper.Scrape();

                CsvWriter.Write(airportList, $"airports_{c}");
            }
            */

            // Write all airports into one file
            string[] urls = new string[LETTERS_IN_ALPHABET];
            for (int i = 0; i < LETTERS_IN_ALPHABET; i++)
            {
                char c = Convert.ToChar(i + ASCII_CODE_A);
                string url = BASE_URL + c;
                urls[i] = url;
            }
            var scraper = new WikiAirportScraper(urls, xpath);
            var airportList = scraper.Scrape();

            CsvWriter.Write(airportList, $"airports");
        }
    }
}