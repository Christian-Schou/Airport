using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Entities
{
    public class Flight
    {
        public Flight()
        {
        }

        public Flight(int flightNumber, string aircraftType, string fromLocation, string toLocation, DateTime arrivalTime, DateTime departureTime)
        {
            FlightNumber = flightNumber;
            AircraftType = aircraftType;
            FromLocation = fromLocation;
            ToLocation = toLocation;
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
        }

        // Primary key in database
        // Identity creation for "on add" - a new key will be generated, 
        // when a gift is added
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightNumber { get; set; }

        [Required]
        public string AircraftType { get; set; }

        [Required]
        public string FromLocation { get; set; }

        [Required]
        public string ToLocation { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }
    }
}
