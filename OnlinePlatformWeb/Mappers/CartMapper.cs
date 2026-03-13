using OnlinePlatformWeb.DataTransferObjects;
using OnlinePlatformWeb.Models;

namespace OnlinePlatformWeb.Mappers;

public static class CartMapper
{
    public static CartDto MapToDto(Cart cart) => new()
    {
        Id = cart.Id,
        Items = cart.Items.Select(i => new CartItemDto
        {
            ProductId = i.ProductId,
            ProductName = i.ProductName,
            UnitPrice = i.UnitPrice,
            Quantity = i.Quantity
        }).ToList(),
        Total = cart.Items.Sum(i => i.UnitPrice * i.Quantity)
    };
}
