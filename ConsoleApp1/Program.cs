﻿// See https://aka.ms/new-console-template for more information

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