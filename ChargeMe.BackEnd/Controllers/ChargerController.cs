using Microsoft.AspNetCore.Mvc;
using ChargeMe.Shared.Models;
using ChargeMe.BackEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace ChargeMe.BackEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChargerController : Controller
{
    private readonly DataContext _context;

    public ChargerController(DataContext context)
    {
        _context = context;
    }
    
    // GET
    [HttpGet("GetAllChargingStations")]
    public async Task<IActionResult> GetAllChargingStations()
    {
        var chargers = await _context.ChargingStations.ToListAsync();

        if (!chargers.Any())
        {
            return BadRequest("Nessuna stazione di ricarica trovata");
        }
        
        return Ok(chargers);
    }
    
    // POST
    [HttpPost("AddChargingStation")]
    public async Task<IActionResult> AddChargingStation(ChargingStation chargingStation)
    {
        try
        {
            await _context.ChargingStations.AddAsync(chargingStation);
            
            await _context.SaveChangesAsync();
        
            return Ok(chargingStation);
            
            
        }catch (Exception e)
        {
            return BadRequest("Errore durante l'aggiunta della stazione di ricarica " + e.Message);
        }
    }
}