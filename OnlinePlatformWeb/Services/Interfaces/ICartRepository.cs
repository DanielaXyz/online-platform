using OnlinePlatformWeb.Models;

namespace OnlinePlatformWeb.Services.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartAsync(string userId);

    Task<Cart> CreateOrUpdateCartAsync(Cart cart);

    Task<bool> DeleteCartAsync(string userId);

    Task AddItemAsync(string userId, Cart item);

    Task RemoveItemAsync(string userId, string productId);
}