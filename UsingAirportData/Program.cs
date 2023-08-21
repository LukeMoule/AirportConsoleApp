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

            var query1 =
                from airport in airportList
                where airport.elevation_ft == -9999
                select airport;

            foreach (var airport in query1)
            {
                Console.WriteLine(airport.name);
            }
        }
    }
    internal class AirportMap : ClassMap<Airport>
    {
        public AirportMap()
        {
            // Default mapping for fields
            AutoMap(CultureInfo.InvariantCulture);
            // if elevation_ft field is empty, default to -9999
            // better way to do this?
            Map(x => x.elevation_ft).Default(-9999);
        }
    }
}