using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ChargeMe.Shared.Models;


namespace ChargeMe.BackEnd.Controllers;

public class ChargingController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ChargingController(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }
    
    
    // GET
    [HttpGet("GetAllCharging")]
    public async Task<IActionResult> GetAllCharging()
    {
        
        return Ok();
    }
    
    // POST 
    [HttpPost("InsertCharging")]
    public async Task<IActionResult> InsertCharging([FromBody] InsertCar car)
    {
        
        return Ok();
    }
    
    //DELETE 
    
    // PUT 
    
    
}