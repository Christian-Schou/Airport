using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Entities.Models
{
    public class FlightDto
    {
        public int FlightNumber { get; set; }

        [Required]
        [Display(Name = "Aircraft type")]
        public string AircraftType { get; set; }

        [Required]
        [Display(Name = "From location")]
        public string FromLocation { get; set; }

        [Display(Name = "To location")]
        public string ToLocation { get; set; }

        [Display(Name = "Departure time")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Arrival time")]
        public DateTime ArrivalTime { get; set; }
    }
}
