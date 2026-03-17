using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Repository;
using Catalog.Domain.Entities;
using Contracts.Requests.Catalog;
using System;
using System.Threading.Tasks;
using static Contracts.Responses.Catalog.FoodResponse;

namespace Catalog.Application.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUnitOfWorkCatalog _uow;

        public FoodService(
            IFoodRepository foodRepository,
            IRestaurantRepository restaurantRepository,
            IUnitOfWorkCatalog unitOfWork)
        {
            _foodRepository = foodRepository;
            _restaurantRepository = restaurantRepository;
            _uow = unitOfWork;
        }

        public async Task<Guid> CreateFoodAsync(Guid ownerId, Guid restaurantId, CreateFoodRequest request)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null) throw new Exception("Restaurant not found.");
            if (restaurant.OwnerId != ownerId) throw new UnauthorizedAccessException("You do not own this restaurant.");

            var food = new Food
            {
                Id = Guid.NewGuid(),
                RestaurantId = restaurantId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsAvailable = true
            };

            _foodRepository.Add(food);
            await _uow.SaveChangesAsync();

            return food.Id;
        }

        public async Task<Guid> AddModifierAsync(Guid ownerId, Guid foodId, AddModifierRequest request)
        {
            var food = await _foodRepository.GetByIdAsync(foodId);
            if (food == null) throw new Exception("Food not found.");

            var restaurant = await _restaurantRepository.GetByIdAsync(food.RestaurantId);
            if (restaurant == null || restaurant.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You do not own the restaurant for this food item.");

            var modifier = new ProductModifier
            {
                Id = Guid.NewGuid(),
                FoodId = foodId,
                Name = request.Name,
                IsRequired = request.IsRequired,
                MinSelection = request.MinSelection,
                MaxSelection = request.MaxSelection
            };

            food.AddProductModifier(modifier);

            _foodRepository.Update(food);
            await _uow.SaveChangesAsync();

            return modifier.Id;
        }

        public async Task<Guid> AddModifierItemAsync(Guid ownerId, Guid modifierId, AddModifierItemRequest request)
        {
            var food = await _foodRepository.GetByModifierIdAsync(modifierId);
            if (food == null) throw new Exception("Parent food or modifier not found.");

            var restaurant = await _restaurantRepository.GetByIdAsync(food.RestaurantId);
            if (restaurant == null || restaurant.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You do not own the restaurant for this item.");

            var modifier = food.ProductModifiers.FirstOrDefault(m => m.Id == modifierId);
            if (modifier == null) throw new Exception("Modifier not found within the food item.");

            var modifierItem = new ModifierItem
            {
                Id = Guid.NewGuid(),
                ProductModifierId = modifierId,
                Name = request.Name,
                PriceAdjustment = request.PriceAdjustment,
                IsActive = true
            };

            modifier.AddModifierEvent(modifierItem);

            _foodRepository.Update(food);
            await _uow.SaveChangesAsync();

            return modifierItem.Id;
        }

        public async Task<IEnumerable<FoodMenuResponse>> GetRestaurantMenuAsync(Guid restaurantId)
        {
            var foods = await _foodRepository.GetByRestaurantIdAsync(restaurantId);

            var menu = foods.Select(f => new FoodMenuResponse
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                Price = f.Price,
                IsAvailable = f.IsAvailable,
                Modifiers = f.ProductModifiers.Select(m => new ModifierResponse
                {
                    Id = m.Id,
                    Name = m.Name,
                    IsRequired = m.IsRequired,
                    MinSelection = m.MinSelection,
                    MaxSelection = m.MaxSelection,
                    Items = m.ModifierItems.Where(i => i.IsActive).Select(i => new ModifierItemResponse
                    {
                        Id = i.Id,
                        Name = i.Name,
                        PriceAdjustment = i.PriceAdjustment
                    }).ToList()
                }).ToList()
            }).ToList();

            return menu;
        }
        public async Task DeleteFoodAsync(Guid ownerId, Guid foodId)
        {
            var food = await _foodRepository.GetByIdAsync(foodId);
            if (food == null) throw new Exception("Food not found.");

            var restaurant = await _restaurantRepository.GetByIdAsync(food.RestaurantId);
            if (restaurant == null || restaurant.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You do not own the restaurant for this food item.");

            food.IsAvailable = false;

            _foodRepository.Update(food);
            await _uow.SaveChangesAsync();
        }
        public async Task UpdateFoodAsync(Guid ownerId, Guid foodId, UpdateFoodRequest request)
        {
            var food = await _foodRepository.GetByIdAsync(foodId);
            if (food == null) throw new Exception("Food not found.");

            var restaurant = await _restaurantRepository.GetByIdAsync(food.RestaurantId);
            if (restaurant == null || restaurant.OwnerId != ownerId)
                throw new UnauthorizedAccessException("Unauthorized access.");

            food.Name = request.Name;
            food.Description = request.Description;
            food.Price = request.Price;
            food.IsAvailable = request.IsAvailable;

            _foodRepository.Update(food);
            await _uow.SaveChangesAsync();
        }
    }
}