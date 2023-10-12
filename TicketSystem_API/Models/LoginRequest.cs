using MongoDB.Bson.Serialization.Attributes;

public class LoginRequest
{
    [BsonElement("email")]
    public string email { get; set; }

    [BsonElement("password")]
    public string password { get; set; }
}