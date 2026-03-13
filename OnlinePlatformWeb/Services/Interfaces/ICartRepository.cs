using OnlinePlatformWeb.Models;

namespace OnlinePlatformWeb.Services.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartAsync(string userId);

    Task<bool> DeleteCartAsync(string userId);

    Task AddItemAsync(string userId, CartItem item);

    Task RemoveItemAsync(string userId, string productId);
}