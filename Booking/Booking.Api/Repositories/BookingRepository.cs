using BookingModel = Booking.Api.Models.Booking;
using System.Collections.Concurrent;

namespace Booking.Api.Repositories;

public class BookingRepository
{
    private readonly ConcurrentDictionary<int, BookingModel> _bookings = new();
    private int _idCounter = 1;

    public IEnumerable<BookingModel> GetAll() => _bookings.Values;
    public BookingModel? Get(int id) => _bookings.TryGetValue(id, out var booking) ? booking : null;
    public BookingModel Add(BookingModel booking)
    {
        booking.Id = _idCounter++;
        _bookings[booking.Id] = booking;
        return booking;
    }
    public bool Update(int id, BookingModel booking)
    {
        if (!_bookings.ContainsKey(id)) return false;
        booking.Id = id;
        _bookings[id] = booking;
        return true;
    }
    public bool Delete(int id) => _bookings.TryRemove(id, out _);
}