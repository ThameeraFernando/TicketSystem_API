using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketSystem_API.Models;
using TicketSystem_API.services;
using Newtonsoft.Json;

namespace TicketSystem_API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService scheduleService;
 
 

        public SchedulesController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

  
        // GET: api/Schedules
        [HttpGet]
        public ActionResult<List<Schedule>> Get()
        {
            var jsonObject = new
            {
               shedule  = scheduleService.Get()
            };

            // Serialize the JSON object to a JSON string
            var jsonResult = JsonConvert.SerializeObject(jsonObject);

            return Ok(jsonResult);
        }

        // GET api/Schedules/5
        [HttpGet("{id}")]
        public ActionResult<Schedule> Get(string id)
        {
            var schedule = scheduleService.Get(id);

            if (schedule == null)
            {
                return NotFound($"Schedule with Id = {id} not found");
            }

            return schedule;
        }

        // POST api/Schedules
        [HttpPost]
        public ActionResult<Schedule> Post([FromBody] Schedule schedule)
        {
            schedule.createdAt = DateTime.Now;
            scheduleService.Create(schedule);

            return CreatedAtAction(nameof(Get), new { id = schedule.id }, schedule);
        }

        // PATCH api/Schedules/5
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody] JsonPatchDocument<ScheduleDTO> partialSchedule)
        {
            var existingSchedule = scheduleService.Get(id);

            if (existingSchedule == null)
            {
                return NotFound($"Schedule with Id = {id} not found");
            }

            // Convert the existing model to a DTO
            var scheduleDTO = new ScheduleDTO
            {
                id = existingSchedule.id,
                name = existingSchedule.name,
                startingPoint = existingSchedule.startingPoint,
                destination = existingSchedule.destination,
                arrivalTimeToEndStation = existingSchedule.arrivalTimeToEndStation,
                departureTimeFromStartStation = existingSchedule.departureTimeFromStartStation,
                ticketPrice = existingSchedule.ticketPrice,
                isActive = existingSchedule.isActive,
                availableSeats = existingSchedule.availableSeats,
                availableDates = existingSchedule.availableDates,
                date = existingSchedule.date,
                availableTimes = existingSchedule.availableTimes
                // Map other fields as needed
            };

            // Apply the patch operations to the DTO
            partialSchedule.ApplyTo(scheduleDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert the updated DTO back to the model
            var updatedSchedule = new Schedule
            {
                id = scheduleDTO.id,
                name = scheduleDTO.name,
                startingPoint = scheduleDTO.startingPoint,
                destination = scheduleDTO.destination,
                arrivalTimeToEndStation = scheduleDTO.arrivalTimeToEndStation,
                departureTimeFromStartStation = scheduleDTO.departureTimeFromStartStation,
                ticketPrice = scheduleDTO.ticketPrice,
                isActive = scheduleDTO.isActive,
                availableSeats = scheduleDTO.availableSeats,
                availableDates = scheduleDTO.availableDates,
                date = scheduleDTO.date,
                availableTimes = scheduleDTO.availableTimes,
                createdAt = existingSchedule.createdAt
                // Map other fields as needed
            };

            // Perform the actual update
            scheduleService.Update(id, updatedSchedule);

            return NoContent();
        }

        // DELETE api/Schedules/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var schedule = scheduleService.Get(id);

            if (schedule == null)
            {
                return NotFound($"Schedule with Id = {id} not found");
            }

            scheduleService.Remove(schedule.id);

            return Ok($"Schedule with Id = {id} deleted");
        }
    }
}