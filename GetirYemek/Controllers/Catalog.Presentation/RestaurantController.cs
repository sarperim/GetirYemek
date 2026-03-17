using Catalog.Application.Interfaces;
using Contracts.Requests.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetirYemek.Controllers.Catalog.Presentation
{
    [Route("api/catalog/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    { 
        private readonly IRestaurantService _restaurantService;
        private readonly IFoodService _foodService;
        public RestaurantController(IRestaurantService restaurantService, IFoodService foodService)
        {
            _restaurantService = restaurantService;
            _foodService = foodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);// fix this with result pattern 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(Guid id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            return Ok(restaurant); // fix this with result pattern
        }

        [HttpGet("{id}/menu")]
        public async Task<IActionResult> GetRestaurantMenu(Guid id)
        {
            var menu = await _foodService.GetRestaurantMenuAsync(id);
            return Ok(menu); // fix this with result pattern
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRestaurant(RestaurantRequest request) 
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid ownerId))
            {
                return Unauthorized(new { Message = "Invalid or missing user identity in token." });
            }

            var newRestaurantId = await _restaurantService.CreateRestaurantAsync(ownerId, request);
            return CreatedAtAction(
                 nameof(GetRestaurantById),
                 new { id = newRestaurantId },
                 new { Message = "Restaurant created successfully", RestaurantId = newRestaurantId }
                 );
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRestaurant(Guid id, object request)
        {
            return Ok(new { Message = "Restaurant updated" });
        }
    }
}