using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TicketSystem_API.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; } = String.Empty;

        [BsonElement("startingPoint")]
        public string startingPoint { get; set; }

        [BsonElement("destination")]
        public string destination { get; set; }

        [BsonElement("date")]
        public string date { get; set; }
        [BsonElement("arrivalTimeToEndStation")]
        public string arrivalTimeToEndStation { get; set; }

        [BsonElement("departureTimeFromStartStation")]
        public string departureTimeFromStartStation { get; set; }

        [BsonElement("availableDates")]
        public DateInfo availableDates { get; set; }

        [BsonElement("availableTimes")]
        public TimeInfo availableTimes { get; set; }

        [BsonElement("userEmail")]
        public string userEmail { get; set; }

        [BsonElement("sheduleID")]
        public string scheduleID { get; set; }

        [BsonElement("nic")]
        public string nic { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)] // Ensure the date is stored as UTC
        public DateTime createdAt { get; set; } 
    }

    public class DateInfo
    {
        [BsonElement("date1")]
        public string date1 { get; set; }

        [BsonElement("date2")]
        public string date2 { get; set; }

        [BsonElement("date3")]
        public string date3 { get; set; }
    }

    public class TimeInfo
    {
        [BsonElement("time1")]
        public string time1 { get; set; }

        [BsonElement("time2")]
        public string time2 { get; set; }

        [BsonElement("time3")]
        public string time3 { get; set; }
    }
}
