using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TicketSystem_API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; } = String.Empty;

        [BsonElement("fullName")]
        public string fullName { get; set; }

        [BsonElement("password")]
        public string password { get; set; }

        [BsonElement("nic")]
        public string nic { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("type")]
        public string type { get; set; } = "Passenger";

        [BsonElement("isActivate")]
        public bool isActivate { get; set; } = true;

        // Other user properties
    }
}