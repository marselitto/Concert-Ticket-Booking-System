using System;
using System.Collections.Generic;
using System.Linq;

public class Concert
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }

    public Concert(string name, DateTime date, string location, int availableSeats)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
    }

    public bool BookSeat()
    {
        if (AvailableSeats > 0)
        {
            AvailableSeats--;
            return true;
        }
        return false;
    }
}

public class BookingSystem
{
    private List<Concert> concerts = new List<Concert>();

    public void AddConcert(Concert concert)
    {
        concerts.Add(concert);
    }

    public void ReserveTicket(string concertName)
    {
        var concert = concerts.FirstOrDefault(c => c.Name == concertName);
        if (concert != null)
        {
            if (concert.BookSeat())
            {
                Console.WriteLine($"Bilet na koncert '{concert.Name}' został zarezerwowany. Pozostało {concert.AvailableSeats} miejsc.");
            }
            else
            {
                Console.WriteLine($"Brak dostępnych miejsc na koncert '{concert.Name}'.");
            }
        }
        else
        {
            Console.WriteLine("Nie znaleziono koncertu o podanej nazwie.");
        }
    }

    public void ShowConcerts()
    {
        Console.WriteLine("Dostępne koncerty:");
        foreach (var concert in concerts)
        {
            Console.WriteLine($"Nazwa: {concert.Name}, Data: {concert.Date.ToShortDateString()}, Lokalizacja: {concert.Location}, Dostępne miejsca: {concert.AvailableSeats}");
        }
    }

    public void AddConcertManually()
    {
        Console.WriteLine("\nDodawanie nowego koncertu:");

        Console.Write("Podaj nazwę koncertu: ");
        string name = Console.ReadLine();

        Console.Write("Podaj datę koncertu (format: dd-mm-yyyy): ");
        DateTime date = DateTime.Parse(Console.ReadLine());

        Console.Write("Podaj lokalizację koncertu: ");
        string location = Console.ReadLine();

        Console.Write("Podaj liczbę dostępnych miejsc: ");
        int availableSeats = int.Parse(Console.ReadLine());

        AddConcert(new Concert(name, date, location, availableSeats));
        Console.WriteLine("Nowy koncert został dodany.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        BookingSystem bookingSystem = new BookingSystem();

        bookingSystem.AddConcert(new Concert("Koncert Rockowy", DateTime.Parse("2024-12-25"), "Warszawa", 50));
        bookingSystem.AddConcert(new Concert("Koncert Jazzowy", DateTime.Parse("2024-12-30"), "Kraków", 30));

        bool running = true;
        while (running)
        {
            Console.Clear();
            bookingSystem.ShowConcerts(); 

            Console.WriteLine("\nWybierz opcję:");
            Console.WriteLine("1 - Dodaj nowy koncert");
            Console.WriteLine("2 - Zarezerwuj bilet");
            Console.WriteLine("3 - Wyświetl dostępne koncerty");
            Console.WriteLine("4 - Zakończ");
            Console.Write("Wybór: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    bookingSystem.AddConcertManually();
                    break;
                case "2":
                    Console.WriteLine("\nRezerwacja biletu na koncert:");
                    bookingSystem.ShowConcerts();
                    Console.Write("Podaj nazwę koncertu, na który chcesz zarezerwować bilet: ");
                    string concertName = Console.ReadLine();
                    bookingSystem.ReserveTicket(concertName);
                    break;
                case "3":
                    bookingSystem.ShowConcerts();
                    break;
                case "4":
                    running = false;
                    Console.WriteLine("Zakończono program.");
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }
}
