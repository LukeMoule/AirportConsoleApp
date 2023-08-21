using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace UsingAirportData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Airport> airportList;

            // Locally stored CSV file containing airport data, source https://ourairports.com/data/
            using (var reader = new StreamReader(@"C:\Users\luke\Desktop\CSharp Learning\Data\airports.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Register the new mapping to handle elevation_ft type error
                csv.Context.RegisterClassMap<AirportMap>();
                airportList = csv.GetRecords<Airport>().ToList();
            }

            foreach (var airport in airportList)
            {
                Console.WriteLine(airport);
            }
        }
    }
    internal class AirportMap : ClassMap<Airport>
    {
        public AirportMap()
        {
            // Default mapping for fields
            AutoMap(CultureInfo.InvariantCulture);
            // if elevation_ft field is empty, default to 0
            Map(x => x.elevation_ft).Default(0);
        }
    }
}