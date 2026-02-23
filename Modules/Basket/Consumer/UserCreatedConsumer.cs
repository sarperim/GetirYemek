using Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks; 

namespace Basket.Consumer
{
    public class UserCreatedConsumer : IConsumer<IUserCreated>
    {
        private readonly IMongoCollection<Domain.Basket> _collection;
        private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(
        IMongoDatabase database,
        ILogger<UserCreatedConsumer> logger)
    {
        _collection = database.GetCollection<Domain.Basket>("baskets");
        _logger = logger;
    }
        public async Task Consume(ConsumeContext<IUserCreated> context)
        {
            var userId = context.Message.UserId;

            var basket = new Domain.Basket
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                await _collection.InsertOneAsync(basket);
            }
            catch (MongoWriteException ex) when (
                ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                // Idempotency: basket already exists do nothing
                _logger.LogInformation("Basket already exists for user {UserId}", userId);
            }
        }
    }
}
