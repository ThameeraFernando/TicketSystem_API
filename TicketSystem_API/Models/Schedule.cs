

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TicketSystem_API.Models;

namespace TicketSystem_API.Models
{
    [BsonIgnoreExtraElements]
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; } = String.Empty;

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("startingPoint")]
        public string startingPoint { get; set; }

        [BsonElement("destination")]
        public string destination { get; set; }

        [BsonElement("arrivalTimeToEndStation")]
        public string arrivalTimeToEndStation { get; set; }

        [BsonElement("departureTimeFromStartStation")]
        public string departureTimeFromStartStation { get; set; }

        [BsonElement("ticketPrice")]
        public double ticketPrice { get; set; }

        [BsonElement("isActive")]
        public bool isActive { get; set; }

        [BsonElement("availableSeats")]
        public int availableSeats { get; set; }

        [BsonElement("availableDates")]
        public List<DateInfo> availableDates { get; set; }

        [BsonElement("date")]
        public string date { get; set; }

        [BsonElement("time")]
        public string time { get; set; }

        [BsonElement("availableTimes")]
        public List<TimeInfo> availableTimes { get; set; }

        public DateTime createdAt { get; set; }

    }


}

