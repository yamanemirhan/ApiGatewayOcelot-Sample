using FlightModel = Flight.Api.Models.Flight;
using System.Collections.Concurrent;

namespace Flight.Api.Repositories;

public class FlightRepository
{
    private readonly ConcurrentDictionary<int, FlightModel> _flights = new();
    private int _idCounter = 1;

    public IEnumerable<FlightModel> GetAll() => _flights.Values;
    public FlightModel? Get(int id) => _flights.TryGetValue(id, out var flight) ? flight : null;
    public FlightModel Add(FlightModel flight)
    {
        flight.Id = _idCounter++;
        _flights[flight.Id] = flight;
        return flight;
    }
    public bool Update(int id, FlightModel flight)
    {
        if (!_flights.ContainsKey(id)) return false;
        flight.Id = id;
        _flights[id] = flight;
        return true;
    }
    public bool Delete(int id) => _flights.TryRemove(id, out _);
}