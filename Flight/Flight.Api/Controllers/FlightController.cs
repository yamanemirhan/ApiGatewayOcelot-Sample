using FlightModel = Flight.Api.Models.Flight;
using Flight.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Flight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightController : ControllerBase
{
    private readonly FlightRepository _repository;
    public FlightController(FlightRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<FlightModel>> GetAll() => Ok(_repository.GetAll());

    [HttpGet("{id}")]
    public ActionResult<FlightModel> Get(int id)
    {
        var flight = _repository.Get(id);
        if (flight is null) return NotFound();
        return Ok(flight);
    }

    [HttpPost]
    public ActionResult<FlightModel> Create(FlightModel flight)
    {
        var created = _repository.Add(flight);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, FlightModel flight)
    {
        if (!_repository.Update(id, flight)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_repository.Delete(id)) return NotFound();
        return NoContent();
    }
}