
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TicketSystem_API.Models;
using TicketSystem_API.services;
using Newtonsoft.Json;
using System.Text.Json.Nodes;


namespace TicketSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService bookingService;

        private readonly ILogger<BookingsController> logger;

 
        public BookingsController(IBookingService bookingService, ILogger<BookingsController> _logger)
        {
            this.bookingService = bookingService;
            logger = _logger;
        }

        // GET: api/Bookings
        [HttpGet]
        public ActionResult<List<Booking>> Get()
        {
            logger.LogInformation("test log");
            return bookingService.Get();
        }

        // GET api/Bookings/5
        [HttpGet("{id}")]
        public ActionResult<Booking> Get(string id)
        {
            var booking = bookingService.Get(id);

            if (booking == null)
            {
                return NotFound($"Booking with Id = {id} not found");
            }

            return booking;
        }

        // POST api/Bookings
        [HttpPost]
        public ActionResult<Booking> Post([FromBody] Booking booking)
        {
                try
            {

                // Log the request body
                logger.LogInformation($"Request Body: {JsonConvert.SerializeObject(booking)}");
                booking.createdAt = DateTime.UtcNow;

                var createdBooking = bookingService.Create(booking);
                return CreatedAtAction(nameof(Get), new { id = createdBooking.id }, createdBooking);
            }
            catch (Exception ex)
            {
                var jsonObject = new
                {
                    msg = ex.Message
                };

                // Serialize the JSON object to a JSON string
                var jsonResult = JsonConvert.SerializeObject(jsonObject);
                // Handle the exception and return a BadRequest response with the error message.
                return BadRequest(jsonResult);
            }
        }

        // PATCH api/Bookings/5
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody] JsonPatchDocument<BookingDTO> partialBooking)
        {
            var existingBooking = bookingService.Get(id);

            if (existingBooking == null)
            {
                return NotFound($"Booking with Id = {id} not found");
            }

            // Convert the existing model to a DTO
            var bookingDTO = new BookingDTO
            {
                id = existingBooking.id,
                startingPoint = existingBooking.startingPoint,
                destination = existingBooking.destination,
                date = existingBooking.date,
                availableDates = existingBooking.availableDates,
                availableTimes = existingBooking.availableTimes,
                userEmail = existingBooking.userEmail,
                scheduleID = existingBooking.scheduleID,
                nic = existingBooking.nic,
                departureTimeFromStartStation = existingBooking.departureTimeFromStartStation,
                arrivalTimeToEndStation = existingBooking.arrivalTimeToEndStation,
                // Map other fields as needed
            };

            // Apply the patch operations to the DTO
            partialBooking.ApplyTo(bookingDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert the updated DTO back to the model
            var updatedBooking = new Booking
            {
                id = bookingDTO.id,
                startingPoint = bookingDTO.startingPoint,
                destination = bookingDTO.destination,
                date = bookingDTO.date,
                departureTimeFromStartStation = bookingDTO.departureTimeFromStartStation,
                arrivalTimeToEndStation = bookingDTO.arrivalTimeToEndStation,
                availableDates = bookingDTO.availableDates,
                availableTimes = bookingDTO.availableTimes,
                userEmail = bookingDTO.userEmail,
                scheduleID = bookingDTO.scheduleID,
                nic = bookingDTO.nic,
                createdAt = existingBooking.createdAt
                // Map other fields as needed
            };

            // Perform the actual update
            bookingService.Update(id, updatedBooking);
            var jsonResult = JsonConvert.SerializeObject(updatedBooking);
            return Ok(jsonResult);
        }

        // DELETE api/Bookings/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var booking = bookingService.Get(id);

            if (booking == null)
            {
                return NotFound($"Booking with Id = {id} not found");
            }

            bookingService.Remove(booking.id);

            return Ok($"Booking with Id = {id} deleted");
        }
        // GET: api/Bookings?userEmail=onlinepoorna@gmail.com
        [HttpGet("filter")]
        public ActionResult<List<Booking>> GetBookingsByUserEmail([FromQuery] string userEmail)
        {
            var bookings = bookingService.GetBookingsByUserEmail(userEmail);


            if (bookings.Count == 0)
            {
                return NotFound($"No bookings found for userEmail = {userEmail}");
            }

            var jsonObject = new
            {
                bookings = bookings
            };

            // Serialize the JSON object to a JSON string
            var jsonResult = JsonConvert.SerializeObject(jsonObject);

            return Ok(jsonResult);
        }
    }
}