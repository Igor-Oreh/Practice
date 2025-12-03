namespace FoodDeliveryService;
public class Courier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string VehicleType { get; set; }
    public List<Delivery> Deliveries { get; set; } = new();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}