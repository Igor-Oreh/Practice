namespace FoodDeliveryService;
public class Delivery
{
    public int Id { get; set; }
    public DateTime DeliveryTime { get; set; }
    public DateTime? ActualDeliveryTime { get; set; }
    public string Status { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int CourierId { get; set; }
    public Courier Courier { get; set; }
}