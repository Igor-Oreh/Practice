class Z1()
{
    public static void Run()
{
        int n = 11;
        int[] arr = new int[n];
        Console.WriteLine("Введите 11 элеиментов массива:");
        for (int i = 0; i < n; i++) {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        int buff;
        for (int i = 0; i < n / 2; i++) {
            buff = arr[i];
            arr[i] = arr[n - i - 1];
            arr[n - i - 1] = buff;    
        }
        Console.WriteLine("Перевернутый массив:");
        foreach (int number in arr)
        {
            Console.Write(number + " "); 
        }
        bool is_palindrom = true;
        for (int i = 0; i < n / 2; i++) {
            if (arr[i] != arr[n - i - 1])
            {
                is_palindrom = false;
                break;
            }
        }
        if (is_palindrom)
        {
            Console.WriteLine("\nЭто палиндром");
        }
        else
        {
            Console.WriteLine("\nЭто не палиндром");
        }
    }
}

public interface IPlaylist
{
    string CurrentSong { get; }
    void Add(string song);
    string Next();
    void Insert(string newSong, string afterSong);
}

class Playlist : IPlaylist
{
    private readonly LinkedList<string> _songs;
    private LinkedListNode<string> _currNode;

    public string CurrentSong
    {
        get
        {
            if (_songs.Count == 0)
            {
                return "Плейлист пуст";   
            }
            return _currNode.Value;
        }
    }

    public Playlist()
    {
        _songs = new LinkedList<string>();
        _currNode = null;
    }
    
    public void Add(string song)
    {   
        _songs.AddLast(song);
        _currNode ??= _songs.Last;
    }
    
    public void Insert(string newSong, string afterSong)
    {
        var targetNode = _songs.Find(afterSong);
        if (targetNode == null)
        {
            throw new ArgumentException($"Песня {afterSong} не найдена в плэйлисте");
        }
        _songs.AddAfter(targetNode, newSong);
    }
    
    public string Next()
    {
        if (_songs.Count == 0)
        {
            return "Плейлист пуст";
        }
            
        _currNode = _currNode?.Next ?? _songs.First;
        return _currNode?.Value ?? "No song";
    }
}

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int Population { get; set; }
    public bool HasMetro { get; set; }

    public City(int id, string name, string country, int population, bool hasMetro)
    {
        Id = id;
        Name = name;
        Country = country;
        Population = population;
        HasMetro = hasMetro;
    }

    public override string ToString()
    {
        return $"ID {Id} {Name} ({Country}) Население: {Population:N0} Метро: {(HasMetro ? "Да" : "Нет")}|";
    }
}

class Z2
{
    public static void Run()
    {
        Playlist playlist = new Playlist();
        Console.WriteLine(playlist.CurrentSong);
        playlist.Add("Killer Queen");
        Console.WriteLine(playlist.CurrentSong);
        playlist.Add("Looking out for you");
        playlist.Add("Рыбак");
        playlist.Add("Random song");
        playlist.Add("Моцарт Лунная соната");
        playlist.Insert("This is definitely not a random song.", "Random song");
        playlist.Next();
        Console.WriteLine(playlist.CurrentSong);
        playlist.Next();
        Console.WriteLine(playlist.CurrentSong);
        playlist.Next();
        Console.WriteLine(playlist.CurrentSong);
        Console.WriteLine(playlist.Next());
        playlist.Next();
        Console.WriteLine(playlist.CurrentSong);
        playlist.Next();
        Console.WriteLine(playlist.CurrentSong);
        playlist.Next();
        Console.WriteLine(playlist.CurrentSong);
        playlist.Next();
        Console.WriteLine(playlist.CurrentSong);
    }
}

class Z3
{
    public static void Run()
    {
        List<City> cities = new List<City>{
            new City(1, "Москва", "Россия", 12600000, true),
            new City(2, "Санкт-Петербург", "Россия", 5400000, true),
            new City(3, "Новосибирск", "Россия", 1620000, true),
            new City(4, "Екатеринбург", "Россия", 1490000, true),
            new City(5, "Берлин", "Германия", 3700000, true),
            new City(6, "Гамбург", "Германия", 1850000, true),
            new City(7, "Мюнхен", "Германия", 1480000, true),
            new City(8, "Кёльн", "Германия", 1080000, false),
            new City(9, "Париж", "Франция", 2140000, true),
            new City(10, "Марсель", "Франция", 860000, true),
            new City(11, "Лион", "Франция", 515000, true),
            new City(12, "Лондон", "Великобритания", 8900000, true),
            new City(13, "Бирмингем", "Великобритания", 1150000, false),
            new City(14, "Рим", "Италия", 2870000, true),
            new City(15, "Милан", "Италия", 1370000, true)
        };
        var millionPeopleCities = cities
                                    .Where(c => c.Population > 1000000)
                                    .OrderBy(c => c.Population);
        Console.WriteLine("Выбрать все города с населением больше 1 миллиона, отсортированные по населению\n");
        foreach (var city in millionPeopleCities)
        {
            Console.WriteLine(city);
        }
        var citiesByCountry = cities
                                .GroupBy(c => c.Country)
                                .Select(g => new
                                {
                                    Country = g.Key,
                                    PeopleCount = g.Sum(c => c.Population),
                                    CityCount = g.Count(),
                                })
                                .OrderByDescending(c => c.PeopleCount);
        Console.WriteLine("Сгруппировать города по странам и найти суммарное население каждой страны.\n");
        foreach (var country in citiesByCountry)
        {
            Console.WriteLine($"{country.Country}: ({country.CityCount} городов {country.PeopleCount} чел. )");
        }
        Console.WriteLine("Проверить, все ли города в стране \"Германия\" имеют метро.\n");
        var GermanHaveMetro = cities.Where(c => c.Country == "Германия").All(c => c.HasMetro);
        if (GermanHaveMetro)
        {
            Console.WriteLine("Все города германии имеют метро");
        }
        else
        {
            Console.WriteLine("Не все города германии имеют метро");
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        //Z1.Run();
        //Z2.Run();
        //Z3.Run();
    }
}