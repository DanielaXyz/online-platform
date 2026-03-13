using Microsoft.AspNetCore.Mvc;
using OnlinePlatformWeb.DataTransferObjects;
using OnlinePlatformWeb.Mappers;
using OnlinePlatformWeb.Models;
using OnlinePlatformWeb.Services.Interfaces;

namespace OnlinePlatformWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
//todo auth
public class CartController(ICartRepository _cartRepo) : ControllerBase
{
    private string UserId => "1";//todo

    [HttpGet]
    public async Task<ActionResult<CartDto>> GetCart()
    {
        var cart = await _cartRepo.GetCartAsync(UserId);
        if (cart == null) return Ok(new CartDto());
        return Ok(CartMapper.MapToDto(cart));
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] AddItemRequest request)
    {
        var item = new CartItem
        {
            ProductId = request.ProductId,
            ProductName = request.ProductName,
            UnitPrice = request.UnitPrice,
            Quantity = request.Quantity
        };

        await _cartRepo.AddItemAsync(UserId, item);
        return Ok();
    }

    [HttpDelete("items/{productId}")]
    public async Task<IActionResult> RemoveItem(string productId)
    {
        await _cartRepo.RemoveItemAsync(UserId, productId);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart()
    {
        await _cartRepo.DeleteCartAsync(UserId);
        return Ok();
    }
}