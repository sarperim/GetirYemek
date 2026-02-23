using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Basket.Domain;

public class Basket
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)] 
    public Guid Id { get; set; }

    [BsonElement("userId")]
    public Guid UserId { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("items")]
    public List<BasketItem> Items { get; set; } = new();
}

public class BasketItem
{
    [BsonElement("productId")]
    public Guid ProductId { get; set; }

    [BsonElement("quantity")]
    public int Quantity { get; set; }
}