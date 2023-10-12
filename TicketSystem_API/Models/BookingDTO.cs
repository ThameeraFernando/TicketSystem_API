using Microsoft.OpenApi.Any;
using MongoDB.Bson.Serialization.Attributes;
using TicketSystem_API.Models;

namespace TicketSystem_API.Models
{
    public class BookingDTO
    {
        public string id { get; set; }  
        public string name { get; set; }
        public string startingPoint { get; set; }
        public string destination { get; set; }
        public DateInfo availableDates { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string nic{ get; set; }
        public string userEmail { get; set; }
        public string scheduleID { get; set; }
        public string arrivalTimeToEndStation { get; set; }

        public string departureTimeFromStartStation { get; set; }
        public TimeInfo availableTimes { get; set; }
    }

}

