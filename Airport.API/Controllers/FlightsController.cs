using Airport.Data;
using Airport.Entities;
using Airport.Entities.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private IUnitOfWork _iUnitOfWork;

        public FlightsController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        [HttpGet()]
        public IActionResult GetAllFlights()
        {
            var flightEntities = _iUnitOfWork.Flight.GetAllFlights();

            // Use the automapper, so we don't have to write all the mappings manually
            var flightResults = Mapper.Map<IEnumerable<Flight>>(flightEntities);

            return Ok(flightResults);
        }

        [HttpGet("{flightNumber}")]
        public IActionResult GetFlight(int flightNumber)
        {
            // First thing to check - did we actually get something back?
            var flight = _iUnitOfWork.Flight.GetFlight(flightNumber);

            // if we didn't return a 404 NotFound HTTP status code
            if (flight == null)
            {
                return NotFound();
            }

            // We map to a Flight with Automapper

            var flightResults = Mapper.Map<Flight>(flight);

            // After that, we return that flight dto
            return Ok(flightResults);
        }

        [HttpGet("{fromLocation}/{toLocation}")]
        public IActionResult GetFlightLocation(string fromLocation, string toLocation)
        {
            // First thing to check - did we actually get something back?
            var flight = _iUnitOfWork.Flight.GetFlightWithLocation(fromLocation, toLocation);

            // if we didn't return a 404 NotFound HTTP status code
            if (flight == null)
            {
                return NotFound();
            }

            // We map to a Flight with Automapper

            var flightResults = Mapper.Map<Flight>(flight);

            // After that, we return that flight dto
            return Ok(flightResults);
        }

        // POST: api/gifts
        [HttpPost]
        public async Task<IActionResult> AddFlight([FromBody] Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _iUnitOfWork.Flight.AddFlight(flight);

            _iUnitOfWork.Complete();

            return Ok();

            //To do.. return the user to the corrosponding flight
            //return CreatedAtAction("GetFlight", new {id = flight.flightNumber}, flight);
        }

        [HttpPut("{flightNumber}")]
        public IActionResult UpdateFlight(int flightNumber,[FromBody] FlightForUpdateDto flight)
        {
            if (flight == null)
            {
                return BadRequest();
            }

            if (flight.DepartureTime == flight.ArrivalTime)
            {
                ModelState.AddModelError("Description", "The departure time and arrival time cannot be the same");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_iUnitOfWork.Flight.FlightExists(flightNumber))
            {
                return NotFound();
            }

            var flightEntity = _iUnitOfWork.Flight.GetFlight(flightNumber);

            Mapper.Map(flight, flightEntity);

            if (!_iUnitOfWork.Flight.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            // Return a 204 No content HTTP status code
            // means that the request completed succesfully, but we got nothing to return
            return NoContent();
        }

        // POST api/auction
        [HttpPost]
        public async Task<IActionResult> UpdateFlight([FromBody] Flight flight)
        {
            var flightItem = _iUnitOfWork.Flight.GetAllFlights().FirstOrDefault(item => item.FlightNumber == flight.FlightNumber);
            
            if (flightItem is null)
            {
                return NotFound("The flight doesn't exist");
            }

                flightItem.FlightNumber = flight.FlightNumber;
                flightItem.AircraftType = flight.AircraftType;
                flightItem.FromLocation = flight.FromLocation;
                flightItem.ToLocation = flight.ToLocation;
                flightItem.DepartureTime = flight.DepartureTime;
                flightItem.ArrivalTime = flight.ArrivalTime;

                _iUnitOfWork.Complete();
               
                return Ok();
            

        }

        private bool FlightExists(int id)
        {
            var flight = _iUnitOfWork.Flight.GetAllFlights().Any(f => f.FlightNumber == id);
            return (flight);
        }

    }
}