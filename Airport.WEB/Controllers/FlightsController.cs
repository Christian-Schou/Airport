using Airport.Data;
using Airport.Entities;
using Airport.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Airport.WEB.Controllers
{
    public class FlightsController : Controller
    {
        // Initialize the HTTP client that retrieves data from our API
        private readonly HttpClient _httpClient;
        //private readonly ApplicationDbContext _applicationDbContext;

        // The base URI for the web api
        private Uri BaseEndPoint { get; set; }

        public FlightsController()
        {
            // Define API url for the api project. You can optain this by starting the project and then copying it
            BaseEndPoint = new Uri("http://localhost:50299/api/flights");
            //_applicationDbContext = applicationDbContext;
            _httpClient = new HttpClient();
        }

        // GET All Flights
        public async Task<IActionResult> Index()
        {
            // use HTTP client to read data from API. Move on once the headers have been read. Errors are caught slightly quicker this way.
            var response = await _httpClient.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);

            // Make sure that we got a success status code in the headers. Returns an exception (and 500 status code) if not successful
            response.EnsureSuccessStatusCode();

            // Turn the response body into a string
            var flightData = await response.Content.ReadAsStringAsync();

            // Treat the response body string as JSON, and deserialize it into a list of gifts
            return View(JsonConvert.DeserializeObject<List<Entities.Models.FlightDto>>(flightData));
        }

        // GET: Flights/DetailsFlight/1
        public async Task<IActionResult> DetailsFlight(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(BaseEndPoint.ToString() + $"/{Id}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var flightData = await response.Content.ReadAsStringAsync();
            var flight = JsonConvert.DeserializeObject<Entities.Models.FlightDto>(flightData);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Manage/1
        public async Task<IActionResult> Manage()
        {
            // use HTTP client to read data from API. Move on once the headers have been read. Errors are caught slightly quicker this way.
            var response = await _httpClient.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);

            // Make sure that we got a success status code in the headers. Returns an exception (and 500 status code) if not successful
            response.EnsureSuccessStatusCode();

            // Turn the response body into a string
            var flightData = await response.Content.ReadAsStringAsync();

            // Treat the response body string as JSON, and deserialize it into a list of gifts
            return View(JsonConvert.DeserializeObject<List<Entities.Models.FlightDto>>(flightData));
        }

        [HttpPost]
        // GET: Fligts/Details/Copenhagen/London
        public async Task<IActionResult> Manage([FromForm] string fromLocation, [FromForm] string toLocation)
        {

            // use HTTP client to read data from API. Move on once the headers have been read. Errors are caught slightly quicker this way.
            var response = await _httpClient.GetAsync(BaseEndPoint + $"/{fromLocation}/{toLocation}", HttpCompletionOption.ResponseHeadersRead);
            
            // Make sure that we got a success status code in the headers. Returns an exception (and 500 status code) if not successful
            response.EnsureSuccessStatusCode();

            // Turn the response body into a string
            var data = await response.Content.ReadAsStringAsync();

            // Treat the response body string as JSON, and deserialize it into a list of flights
            return View(JsonConvert.DeserializeObject<List<Entities.Models.FlightDto>>(data));
        }

        // GET: Flights/AddFlight
        public IActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFlight([Bind("FlightNumber,AircraftType,FromLocation, ToLocation, DepartureTime, ArrivalTime")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                // Post the created gift as JSON to API. HttpClient handles serialization for us
                var response = await _httpClient.PostAsJsonAsync<Flight>(BaseEndPoint, flight);

                // Make sure we got a success, otherwise return 500
                response.EnsureSuccessStatusCode();

                // Redirect back to overview page
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var response = await _httpClient.GetAsync(BaseEndPoint.ToString() + $"/{id}",
                HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var flightData = await response.Content.ReadAsStringAsync();
            var flightItem = JsonConvert.DeserializeObject<Flight>(flightData);

            if (flightItem == null)
            {
                return RedirectToAction("Index");
            }

            return View(flightItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("FlightId,AircraftType,FromLocation,ToLocation,DepartureTime,ArrivalTime")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync<Flight>(BaseEndPoint + $"/{id}", flight);
                    response.EnsureSuccessStatusCode();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }

                return RedirectToAction(nameof(Index));
            }

            return View(flight);
        }

        //private bool FlightItemExists(int id)
        //{
        //    return _applicationDbContext.Flights.Any(f => f.FlightNumber == id);
        //}


    }
}
