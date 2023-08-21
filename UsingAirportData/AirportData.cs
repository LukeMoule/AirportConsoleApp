using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingAirportData
{
    internal class AirportData
    {
        private List<Airport> allAirports;
        private List<Airport> filteredAirports;
        public AirportData(List<Airport> airportList)
        {
            allAirports = new List<Airport>(airportList);
            filteredAirports = new List<Airport>(airportList);
        }

    }
}
