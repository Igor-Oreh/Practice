namespace FoodDeliveryService;
public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } 
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int CourierId { get; set; }        
    public Courier Courier { get; set; }      
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public Delivery Delivery { get; set; }
    
}