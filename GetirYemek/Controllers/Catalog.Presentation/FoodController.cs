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
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;
        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodById(Guid id)
        {
            return Ok(new { Message = $"Details for food {id}" });
        }

        [HttpPost("restaurant/{restaurantId}")]
        [Authorize]
        public async Task<IActionResult> CreateFood(Guid restaurantId, [FromBody] CreateFoodRequest request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out Guid ownerId)) return Unauthorized();

            var newFoodId = await _foodService.CreateFoodAsync(ownerId, restaurantId, request);

            return CreatedAtAction(nameof(GetFoodById), new { id = newFoodId }, new { Message = "Food created", Id = newFoodId });
        }
        // Drink Choice etc.
        [HttpPost("{foodId}/modifiers")]
        [Authorize]
        public async Task<IActionResult> AddModifier(Guid foodId, [FromBody] AddModifierRequest request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out Guid ownerId)) return Unauthorized();

            var newModifierId = await _foodService.AddModifierAsync(ownerId, foodId, request);

            return Ok(new { Message = "Modifier added to food", Id = newModifierId });
        }

        [HttpPost("modifiers/{modifierId}/items")]
        [Authorize]
        public async Task<IActionResult> AddModifierItem(Guid modifierId, [FromBody] AddModifierItemRequest request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out Guid ownerId)) return Unauthorized();

            var newItemId = await _foodService.AddModifierItemAsync(ownerId, modifierId, request);

            return Ok(new { Message = "Item added to modifier", Id = newItemId });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateFood(Guid id, [FromBody] UpdateFoodRequest request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out Guid ownerId)) return Unauthorized();

            await _foodService.UpdateFoodAsync(ownerId, id, request);

            return Ok(new { Message = "Food updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFood(Guid id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out Guid ownerId)) return Unauthorized();

            await _foodService.DeleteFoodAsync(ownerId, id);

            return Ok(new { Message = "Food deleted." });
        }



        //Drink Choice
        [HttpPost("modifiers/{modifierId}/items")]
        [Authorize]
        public async Task<IActionResult> AddModifierItem(Guid modifierId, [FromBody] object request)
        {
            return Ok(new { Message = "Item added to modifier" });
        }
    }
}