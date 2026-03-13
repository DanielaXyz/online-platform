namespace OnlinePlatformWeb.DataTransferObjects;

public class CartDto
{
    public Guid Id { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
    public decimal Total { get; set; }
}