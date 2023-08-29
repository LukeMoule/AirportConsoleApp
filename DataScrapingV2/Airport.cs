using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataScrapingV2
{
    internal class Airport
    {
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public string AirportName { get; set; }
        public string LocationServed { get; set; }
        public string AirportType { get; set; }
        public string Elevation { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int LongestRunway { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize<Airport>(this);
        }
    }
}
