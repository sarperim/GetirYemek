using Basket.Service;
using Contracts.Requests.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GetirYemek.Controllers.Basket.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly BasketService _basketService;

        public BasketController(BasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("add-items")]
        public async Task<IActionResult> AddItem(AddToBasketRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user token.");
            }

            await _basketService.AddItemAsync(userId, request.ProductId, request.Quantity);

            return Ok(new { Message = "Item added to basket successfully" });
        }

        [HttpDelete("items/remove/{productId}")]
        public async Task<IActionResult> RemoveItem(Guid productId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user token.");
            }

            await _basketService.RemoveItemAsync(userId, productId);

            return Ok(new { Message = "Item removed from basket successfully" });
        }

        [HttpPut("items/quantity/{productId}")]
        public async Task<IActionResult> UpdateItemQuantity([FromRoute] Guid productId, [FromBody] UpdateBasketItemQuantityRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user token.");
            }


            await _basketService.UpdateItemQuantityAsync(userId, productId, request.Quantity);

            return Ok(new { Message = "Item quantity updated successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user token.");
            }

            var basket = await _basketService.GetBasketAsync(userId);

            if (basket == null)
            {
                return NotFound(new { Message = "Basket not found." });
            }

            return Ok(basket);
        }


    }
}
