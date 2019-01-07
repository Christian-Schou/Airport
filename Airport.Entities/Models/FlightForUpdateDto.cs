using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Entities.Models
{
    public class FlightForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a departure time")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "You should provide an arrival time")]
        public DateTime ArrivalTime { get; set; }

    }
}
