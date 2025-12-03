namespace FoodDeliveryService;
class Program
{
    static void Main()
    {
        using var context = new AppDbContext();
        

        // Создаем и заносим в бд класс ресторана Олега на улице Олега
        var restaurant = new Restaurant
        {
            Name = "У Олега",
            Address = "ул. Олега, 1",
            Phone = "+375 33 01-59-333"
        }; 

        context.Restaurants.Add(restaurant);
        context.SaveChanges();
        Console.WriteLine("Был создан ресторан: " + restaurant.Name);

        // Создаем нашего клиента Ольгу
        var olga = new Customer
            {
                Name = "Олег Ольга Олеговна",
                Phone = "+375 33 12-34-567",
                Address = "ул. Олега, 2, кв. 1",
                Email = "olga@example.com"
            };

        context.Customers.Add(olga);
        Console.WriteLine($" Создана Ольга: {olga.Name}");

        // Создание курьераа Олега ходящего пешком
        var oleg = new Courier
            {
                Name = "Олег Олег Олегович",
                Phone = "+375 33 45-54-111",
                VehicleType = "Пешком"
            };


        context.Couriers.Add(oleg);
        Console.WriteLine($"Создан Олег: {oleg.Name}");


        // Заполняем меню Олега
        var menuItems = new List<MenuItem>
            {
                new MenuItem { Name = "Пельмени Олега", Description = "Пельмени", Price = 15, RestaurantId = restaurant.Id },
                new MenuItem { Name = "Борщ Олега", Description = "Борщ", Price = 12, RestaurantId = restaurant.Id },
                new MenuItem { Name = "Компот", Description = "от Олега", Price = 5, RestaurantId = restaurant.Id }
            };

        context.MenuItems.AddRange(menuItems);
        context.SaveChanges();
        Console.WriteLine("Заполняем меню");


        //Создаем заказ Ольги из всего меню (15 + 12 + 5)
        var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = 32, 
                Status = "В процессе",
                CustomerId = olga.Id,
                RestaurantId = restaurant.Id,
                CourierId = oleg.Id
            };

        context.Orders.Add(order);
        context.SaveChanges();
        Console.WriteLine($"Ольга создала заказ #{order.Id} на сумму {order.TotalAmount} уе.");


        // Создадим доставку Олега Ольге
        var delivery = new Delivery
            {
                OrderId = order.Id,
                CourierId = oleg.Id,
                DeliveryTime = DateTime.Now.AddHours(1),
                Status = "В пути",
                ActualDeliveryTime = null
            };

        context.Deliveries.Add(delivery);
        context.SaveChanges();

        // Создаем отзыв Ольги на ресторан Олега
        var review = new Review
            {
                CustomerId = olga.Id,
                RestaurantId = restaurant.Id,
                Rating = 5,
                Comment = "У Олега всегда вкусно!",
                CreatedAt = DateTime.Now
            };
        context.Reviews.Add(review);
        context.SaveChanges();

        
        // Последние отзывы
        Console.WriteLine("\nПоследние отзывы:");
        var recentReviews = context.Reviews
            .Where(r => r.RestaurantId == restaurant.Id)
            .OrderByDescending(r => r.CreatedAt)
            .Take(2)
            .ToList();
        
        foreach (var rev in recentReviews)
        {
            Console.WriteLine($"  {rev.Rating} - {rev.Comment} ({rev.CreatedAt:dd.MM.yyyy})");
        }
        // Вывод меню
        var allMenuItems = context.MenuItems.ToList();
        foreach (var item in allMenuItems)
        {
            Console.WriteLine($"{item.Restaurant}: {item.Name} - {item.Price} уе.");
        }
    }
}