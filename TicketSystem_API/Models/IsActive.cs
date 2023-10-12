using MongoDB.Bson.Serialization.Attributes;

public class IsActivate
{
    [BsonElement("isActivate")]
    public bool isActivate { get; set; }

}