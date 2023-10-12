using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TicketSystem_API.Models
{
    public class ScheduleDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string startingPoint { get; set; }
        public string destination { get; set; }
        public string arrivalTimeToEndStation { get; set; }
        public string departureTimeFromStartStation { get; set; }
        public double ticketPrice { get; set; }
        public bool isActive { get; set; }
        public int availableSeats { get; set; }
        public List<DateInfo> availableDates { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public List<TimeInfo> availableTimes { get; set; }
    }

   
}