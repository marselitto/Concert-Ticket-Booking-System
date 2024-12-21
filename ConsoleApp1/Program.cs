// See https://aka.ms/new-console-template for more information

public interface IConcert
{
    string Name { get; }
    DateTime Date { get; }
    string Location { get; }
    int AvailableSeats { get; }
    void BookSeat();
}

public class Concert : IConcert
{
    public string Name { get; private set; }
    public DateTime Date { get; private set; }
    public string Location { get; private set; }
    public int AvailableSeats { get; private set; }

    public Concert(string name, DateTime date, string location, int availableSeats)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
    }

    public virtual void BookSeat()
    {
        if (AvailableSeats > 0)
        {
            AvailableSeats--;
            Console.WriteLine($"Miejsce wykupione przez {Name} na {Date.ToShortDateString()}.");
        }
        else
        {
            Console.WriteLine("Brak wolnych miejsc.");
        }
    }
}

public class RegularConcert : Concert
{
    public RegularConcert(string name, DateTime date, string location, int availableSeats) : base(name, date, location, availableSeats) { }
}

public class VIPConcert : Concert
{
    public VIPConcert(string name, DateTime date, string location, int availableSeats) : base(name, date, location, availableSeats) { }

    public override void BookSeat()
    {
        if (AvailableSeats > 0)
        {
            AvailableSeats--;
            Console.WriteLine($"Siedzenia VIP wykupione przez {Name}.");
        }
        else
        {
            Console.WriteLine("Brak miejsc VIP.");
        }
    }
}

public class OnlineConcert : Concert
{
    public string StreamingPlatform { get; private set; }

    public OnlineConcert(string name, DateTime date, string streamingPlatform)
        : base(name, date, "Online", int.MaxValue)
    {
        StreamingPlatform = streamingPlatform;
    }

    public override void BookSeat()
    {
        Console.WriteLine($"Access booked for {Name} on {StreamingPlatform}.");
    }
}

public class PrivateConcert : Concert
{
    public PrivateConcert(string name, DateTime date, string location, int availableSeats) : base(name, date, location, availableSeats) { }

    public override void BookSeat()
    {
        Console.WriteLine("Aby kupic bilet nalezy posiadac specjalne zaproszenie.");
    }
}

public class Ticket
{
    public IConcert Concert { get; private set; }
    public decimal Price { get; private set; }
    public int SeatNumber { get; private set; }

    public Ticket(IConcert concert, decimal price, int seatNumber)
    {
        Concert = concert;
        Price = price;
        SeatNumber = seatNumber;
    }
}

public class BookingSystem
{
    private List<IConcert> concerts = new List<IConcert>();

    public void AddConcert(IConcert concert)
    {
        concerts.Add(concert);
    }

    public void BookTicket(IConcert concert)
    {
        concert.BookSeat();
    }

    public void DisplayConcerts(Func<IConcert, bool> filter)
    {
        var filteredConcerts = concerts.Where(filter);
        foreach (var concert in filteredConcerts)
        {
            Console.WriteLine($"{concert.Name} - {concert.Date.ToShortDateString()} - {concert.Location} - Miejsca: {concert.AvailableSeats}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BookingSystem bookingSystem = new BookingSystem();
        
        bookingSystem.AddConcert(new RegularConcert("Guns N' Roses", DateTime.Now.AddDays(10), "Stadion Narodowy", 59000));
        bookingSystem.AddConcert(new VIPConcert("Exclusive Jazz", DateTime.Now.AddDays(5), "Jazz Club", 20));
        bookingSystem.AddConcert(new OnlineConcert("Live DJa", DateTime.Now.AddDays(2), "Tiktok"));
        bookingSystem.AddConcert(new PrivateConcert("Wieczór akustyczny", DateTime.Now.AddDays(15), "nad jeziorem", 15));
        
        Console.WriteLine("Koncerty na Stadionie Narodowym:");
        bookingSystem.DisplayConcerts(Concert => Concert.Location == "Stadion Narodowy");
        
        var concert = bookingSystem.DisplayConcerts(Concert => Concert.Name == "Guns N' Roses");
        bookingSystem.BookTicket(concert);
    }
}
