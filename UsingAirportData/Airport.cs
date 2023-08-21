using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace UsingAirportData
{
    internal class Airport
    {
        // Possible to generate this from csv headers?
        public int id { get; set; }
        public string? ident { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public decimal latitude_deg { get; set; }
        public decimal longitude_deg { get; set; }
        public int elevation_ft { get; set; }
        public string? continent { get; set; }
        public string? iso_country { get; set; }
        public string? iso_region { get; set; }
        public string? municipality { get; set; }
        public string? scheduled_service { get; set; }
        public string? gps_code { get; set; }
        public string? iata_code { get; set; }
        public string? local_code { get; set; }
        public string? home_link { get; set; }
        public string? wikipedia_link { get; set; }
        public string? keywords { get; set; }

        public override string ToString()
        {
            // Much nicer than building a long string
            return JsonSerializer.Serialize<Airport>(this);    
        }
    }
}
