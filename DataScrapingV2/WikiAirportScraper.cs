using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataScrapingV2
{
    internal class WikiAirportScraper
    {
        private string[] _urls;

        public WikiAirportScraper(string[] urls)
        {
            _urls = urls;
        }

        public async Task<List<Airport>> Scrape()
        {
            List<Airport> airports = new List<Airport>();
            foreach (string url in _urls)
            {
                List<Airport> airportList = await ScrapeOne(url);
                airports.AddRange(airportList);
            }
            return airports;
        }

        private async Task<List<Airport>> ScrapeOne(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);
            using var document = await context.OpenAsync(url);
            var tableRows = document.QuerySelectorAll("#mw-content-text .wikitable tbody tr:nth-child(n+2):not(.sortbottom)");

            List<Task<string>> tasks = new List<Task<string>>();
            foreach (var row in tableRows)
            {
                tasks.Add(GetRowData(row));
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var t in tasks)
            {
                var res = t.Result;
                Console.WriteLine(res);
            }





            return new List<Airport>();
        }

        private async Task<string> GetRowData(IElement row)
        {
            return await Task.Run(async () =>
            {
                var config = Configuration.Default.WithDefaultLoader();
                using var context = BrowsingContext.New(config);
                string BASE_URL = "https://en.wikipedia.org";

                var cols = row.QuerySelectorAll("td");

                string IATA = cols[0].Text();
                Console.WriteLine(IATA);
                string ICAO = cols[1].Text();
                string Name = cols[2].Text();
                string LocationServed = cols[3].Text();
                string url = cols[2].QuerySelector("a")?.GetAttribute("href");
                string newUrl = BASE_URL + url;
                var airportPage = await context.OpenAsync(newUrl);
                string elevation = HandlePage(airportPage);
                return IATA + " " + elevation;
            });
        }

        private string HandlePage(IDocument document)
        {
            //var airportType = document.QuerySelectorAll(".infobox-label").FirstOrDefault(e => e.Text().Contains("Airport type"))?.NextElementSibling.Text();

            string? elevation = document.QuerySelectorAll(".infobox-label").FirstOrDefault(e => e.Text().Contains("Elevation"))?.NextElementSibling.Text();

            //var latitude = document.QuerySelector(".latitude")?.Text();

            //var longitude = document.QuerySelector(".longitude")?.Text();

            //try
            //{
            //    var x = document.QuerySelectorAll(".infobox-header").FirstOrDefault(e => e.Text().Contains("Runways")).ParentElement.NextElementSibling.QuerySelectorAll("tr:nth-child(n+3)");
            //    foreach (var e in x)
            //    {
            //        e.QuerySelector("td:nth-child(3)").Text().Trim();
            //    }
            //}
            //catch
            //{

            //}
            return elevation;
        }
    }
}
