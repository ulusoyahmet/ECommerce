using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IRepository<Cart> _cartRepository;

        public CartsController(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cart>>> GetAllCarts()
        {
            var carts = await _cartRepository.GetAllAsync();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(string id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Cart>> GetCartByUserId(string userId)
        {
            var cart = await _cartRepository.GetByFilterAsync(c => c.UserId == userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            var createdCart = await _cartRepository.CreateAsync(cart);
            return CreatedAtAction(nameof(GetCart), new { id = createdCart.Id }, createdCart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(string id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _cartRepository.UpdateAsync(cart);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(string id)
        {
            await _cartRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 