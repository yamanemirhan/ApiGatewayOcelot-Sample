using BookingModel = Booking.Api.Models.Booking;
using Booking.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingRepository _repository;
    public BookingController(BookingRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookingModel>> GetAll() => Ok(_repository.GetAll());

    [HttpGet("{id}")]
    public ActionResult<BookingModel> Get(int id)
    {
        var booking = _repository.Get(id);
        if (booking is null) return NotFound();
        return Ok(booking);
    }

    [HttpPost]
    public ActionResult<BookingModel> Create(BookingModel booking)
    {
        var created = _repository.Add(booking);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BookingModel booking)
    {
        if (!_repository.Update(id, booking)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_repository.Delete(id)) return NotFound();
        return NoContent();
    }
}