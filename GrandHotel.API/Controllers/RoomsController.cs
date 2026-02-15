using Microsoft.AspNetCore.Mvc;

namespace GrandHotel.API.Controllers;

/// <summary>
/// Manages hotel room information (hardcoded seed data for demo purposes).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private static readonly List<HotelRoom> Rooms =
    [
        new() { Id = 1, Name = "Ocean View Suite",     RoomNumber = "101", Type = "Suite",    Price = 350.00m, Status = "Available"  },
        new() { Id = 2, Name = "Mountain Deluxe",      RoomNumber = "202", Type = "Deluxe",   Price = 220.00m, Status = "Available"  },
        new() { Id = 3, Name = "Presidential Suite",    RoomNumber = "301", Type = "Suite",    Price = 750.00m, Status = "Occupied"   },
        new() { Id = 4, Name = "Standard Twin",         RoomNumber = "102", Type = "Standard", Price = 120.00m, Status = "Available"  },
        new() { Id = 5, Name = "Garden View Room",      RoomNumber = "103", Type = "Standard", Price = 140.00m, Status = "Maintenance"},
        new() { Id = 6, Name = "Royal Penthouse",       RoomNumber = "501", Type = "Penthouse",Price = 1200.00m,Status = "Available"  },
    ];

    /// <summary>
    /// GET /api/rooms
    /// Returns the full catalogue of hotel rooms.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<HotelRoom>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(Rooms);
    }

    /// <summary>
    /// GET /api/rooms/{id}
    /// Returns a single room by its Id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(HotelRoom), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == id);
        return room is null ? NotFound(new { message = $"Room with Id {id} not found." }) : Ok(room);
    }
}

/// <summary>
/// Represents a hotel room in the Grand Hotel system.
/// </summary>
public class HotelRoom
{
    public int     Id         { get; set; }
    public string  Name       { get; set; } = default!;
    public string  RoomNumber { get; set; } = default!;
    public string  Type       { get; set; } = default!;
    public decimal Price      { get; set; }
    public string  Status     { get; set; } = default!;
}
