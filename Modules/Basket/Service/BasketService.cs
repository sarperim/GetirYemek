using Basket.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Service
{
    public class BasketService
    {
        private readonly IMongoCollection<Domain.Basket> _collection;

        public BasketService(IMongoDatabase database)
        {
            _collection = database.GetCollection<Domain.Basket>("baskets");
        }

        public async Task AddItemAsync(Guid userId, Guid productId, int quantity)
        {
            var filter = Builders<Domain.Basket>.Filter.Eq(x => x.UserId, userId);
            var basket = await _collection.Find(filter).FirstOrDefaultAsync();

            if (basket == null)
            {
                throw new Exception("Basket not found for user."); // it shouldn't happpen
            }

            var existingItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                basket.Items.Add(new BasketItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _collection.ReplaceOneAsync(filter, basket);
        }
        public async Task RemoveItemAsync(Guid userId, Guid productId)
        {
            var filter = Builders<Domain.Basket>.Filter.Eq(x => x.UserId, userId);
            var basket = await _collection.Find(filter).FirstOrDefaultAsync();
            if (basket == null)
            {
                return;
            }

            var itemToRemove = basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if (itemToRemove != null)
            {
                basket.Items.Remove(itemToRemove);

                await _collection.ReplaceOneAsync(filter, basket);
            }
        }

        public async Task UpdateItemQuantityAsync(Guid userId, Guid productId, int quantity)
        {
            var filter = Builders<Domain.Basket>.Filter.Eq(x => x.UserId, userId);
            var basket = await _collection.Find(filter).FirstOrDefaultAsync();

            if (basket == null)
            {
                return;
            }

            var itemToUpdate = basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if (itemToUpdate != null)
            {
                if (quantity <= 0)
                {
                    basket.Items.Remove(itemToUpdate);
                }
                else
                {
                    itemToUpdate.Quantity = quantity;
                }

                await _collection.ReplaceOneAsync(filter, basket);
            }
        }

        
        public async Task<Domain.Basket> GetBasketAsync(Guid userId)
        {
            var filter = Builders<Domain.Basket>.Filter.Eq(x => x.UserId, userId);
            var basket = await _collection.Find(filter).FirstOrDefaultAsync();

            return basket;
        }
    }
}
