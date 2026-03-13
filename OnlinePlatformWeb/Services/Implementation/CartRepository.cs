using Microsoft.EntityFrameworkCore;
using OnlinePlatformWeb.Data;
using OnlinePlatformWeb.Models;
using OnlinePlatformWeb.Services.Interfaces;

namespace ShoppingCartService.Services;

public class CartRepository(CartDbContext _context, ILogger<CartRepository> _logger) : ICartRepository
{
    public async Task<Cart?> GetCartAsync(string userId)
    {
        _logger.LogDebug($"Getting cart for user {userId}.");

        var dbCart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return dbCart;
    }

    public async Task<bool> DeleteCartAsync(string userId)
    {
        _logger.LogDebug($"Deleting cart for user {userId}.");

        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null) return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task AddItemAsync(string userId, CartItem item)
    {
        _logger.LogDebug($"Adding item to cart for user {userId}.");

        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart//check when multiple users
            {
                UserId = userId,
                Id = Guid.NewGuid(),
                LastUpdated = DateTime.UtcNow,
                Items = new List<CartItem>()
            };

            item.Id = Guid.NewGuid();
            item.CartId = cart.Id;
            cart.Items.Add(item);

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return;
        }

        if (_context.Entry(cart).State == EntityState.Detached)
        {
            _context.Carts.Attach(cart);
        }

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
            _context.Entry(existingItem).State = EntityState.Modified;
        }
        else
        {
            item.Id = Guid.NewGuid();
            item.CartId = cart.Id;
            cart.Items.Add(item);
            _context.Entry(item).State = EntityState.Added;
        }

        cart.LastUpdated = DateTime.UtcNow;
        _context.Entry(cart).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(string userId, string productId)
    {
        _logger.LogDebug($"Removing item from cart for user {userId}.");

        var cart = await GetCartAsync(userId);
        if (cart == null) return;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null) return;

        cart.Items.Remove(item);

        await _context.SaveChangesAsync();
    }
}