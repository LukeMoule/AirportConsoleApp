/*
 TODO: Error checking / input validation
 */
using CsvHelper;
using System.Globalization;

namespace AirportConsoleApp
{
    internal class CsvWriter
    {
        public static void Write(List<Airport> airportList, string filename)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string csvFolder = Path.Combine(baseDirectory, "data", "csv");
            if (!Directory.Exists(csvFolder))
            {
                Directory.CreateDirectory(csvFolder);
            }
            string csvFile = Path.Combine(csvFolder, $"{filename}.csv");

            using (var writer = new StreamWriter(csvFile))
            using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(airportList);
            }
        }
    }
}
