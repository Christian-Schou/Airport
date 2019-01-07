using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.Entities;

namespace Airport.Data.IRepository
{
    public interface IFlightRepository
    {
        bool FlightExists(int flightNumber);
        IEnumerable<Flight> GetAllFlights();

        Flight GetFlight(int flightNumber);

        IEnumerable<Flight> GetFlightWithLocation(string fromLocation, string toLocation);

        void AddFlight(Flight flight);

        void DeleteFlight(Flight flight);

        bool Save();
    }
}
