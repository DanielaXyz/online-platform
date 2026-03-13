using OnlinePlatformWeb.Models;
using OnlinePlatformWeb.Services.Interfaces;

namespace ShoppingCartService.Services;

public class CartRepository() : ICartRepository
{
    public async Task<Cart?> GetCartAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Cart> CreateOrUpdateCartAsync(Cart cart)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCartAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task AddItemAsync(string userId, Cart item)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveItemAsync(string userId, string productId)
    {
        throw new NotImplementedException();
    }
}