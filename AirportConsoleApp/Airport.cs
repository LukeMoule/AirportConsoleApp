namespace AirportConsoleApp
{
    internal class Airport
    {
        public string Iata { get; set; }
        public string Icao { get; set; }
        public string AirportName { get; set; }
        public string LocationServed { get; set; }
        public string Time { get; set; }
        public string Dst { get; set; }

        public override string ToString()
        {
            return Iata + Icao + AirportName + LocationServed + Time + Dst;
        }
    }
}
