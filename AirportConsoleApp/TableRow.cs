namespace AirportConsoleApp
{
    internal class TableRow
    {
        public string Iata { get;}
        public string Icao { get; }
        public string AirportName { get; }
        public string LocationServed { get; }
        public string Time { get; }
        public string Dst { get; }

        public TableRow(string iata, string icao, string airportName, string locationServed, string time, string dst)
        {
            Iata = iata;
            Icao = icao;
            AirportName = airportName;
            LocationServed = locationServed;
            Time = time;
            Dst = dst;
        }
    }
}
