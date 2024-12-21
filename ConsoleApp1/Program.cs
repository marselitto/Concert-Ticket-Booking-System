// See https://aka.ms/new-console-template for more information

public interface ConcertInterface
{
    string Name { get; }
    DateTime Date { get; }
    string Location { get; }
    int AvailableSeats { get; }
    void BookSeat();
}