using Airport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Data
{
    public static class AirportContextExtensions
    {
        // This seeding will be executing from startup.cs

        // "this" tells the compiler it extends ApplicationDbContext 
        public static void EnsureSeedDataForContext(this ApplicationDbContext context)
        {
            // if database contain data, we will not insert new data
            if (context.Flights.Any())
            {
                return;
            }

            // init seed data

            var flights = new List<Flight>()
            {

                // Copenhagen - Amsterdam
                new Flight()
                {
                    AircraftType = "Boing 777 Jet",
                    FromLocation = "Copenhagen",
                    ToLocation = "Amsterdam",
                    DepartureTime = DateTime.Today,
                    ArrivalTime = DateTime.Today.AddHours(2)

                },

                // Copenhagen - London
                new Flight()
                {
                    AircraftType = "Boing 777 Jet",
                    FromLocation = "Copenhagen",
                    ToLocation = "London",
                    DepartureTime = DateTime.Today.AddHours(5),
                    ArrivalTime = DateTime.Today.AddHours(6)
                },

                // Copenhagen - London
                new Flight()
                {
                    AircraftType = "Boing 777 Jet",
                    FromLocation = "Paris",
                    ToLocation = "Australia",
                    DepartureTime = DateTime.Today.AddHours(8),
                    ArrivalTime = DateTime.Today.AddDays(1)
                },

            };

            // Lets add the gifts to the context
            context.Flights.AddRange(flights);

            // Then save them to database
            context.SaveChanges();
        }
    }
}
