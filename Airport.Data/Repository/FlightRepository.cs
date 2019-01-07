using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.Data.IRepository;
using Airport.Entities;

namespace Airport.Data.Repository
{
    public class FlightRepository : IFlightRepository
    {
        // Through constructor injection, we are sure, that
        // we have an instance of the ApplicationDbContext
        private readonly ApplicationDbContext _applicationDbContext;

        public FlightRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            // remember .ToList to make sure the query is executed at that specific moment
            // Calling tolist means iteration and for that to happen we have to make a query
            return _applicationDbContext.Flights.OrderBy(x => x.FlightNumber).ToList();
        }

        public bool FlightExists(int flightNumber)
        {
            return _applicationDbContext.Flights.Any(g => g.FlightNumber == flightNumber);
        }

        public Flight GetFlight(int flightNumber)
        {
            // return the specified flight
            return _applicationDbContext.Flights.FirstOrDefault(g => g.FlightNumber == flightNumber);
        }

        public IEnumerable<Flight> GetFlightWithLocation(string fromLocation, string toLocation)
        {
            // return the specified flight
            var flights = _applicationDbContext.Flights.Where(flight => flight.FromLocation == fromLocation && flight.ToLocation == toLocation);
            return flights;
        }

        public void DeleteFlight(Flight flight)
        {
            _applicationDbContext.Flights.Remove(flight);
        }

        public void AddFlight(Flight flight)
        {
            _applicationDbContext.Flights.Add(flight);
        }

        public bool Save()
        {
            // this return the amount of entities that have been changed
            // this means that our method returns true, when 0 or mere entities
            // have been saved to the database
            return (_applicationDbContext.SaveChanges() >= 0);
        }
    }
}
