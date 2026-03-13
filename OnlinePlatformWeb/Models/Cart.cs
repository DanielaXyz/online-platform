namespace OnlinePlatformWeb.Models;

public class Cart
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = new();
    public DateTime LastUpdated { get; set; }
}