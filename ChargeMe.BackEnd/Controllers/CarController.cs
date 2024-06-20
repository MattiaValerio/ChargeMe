using ChargeMe.BackEnd.Migrations;
using ChargeMe.BackEnd.Services.CarServices;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ChargeMe.Shared.Models;
using ChargeMe.BackEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace ChargeMe.BackEnd.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly DataContext _context;

    public CarController(DataContext context)
    {
        _context = context;
    }
    
    // GET
    [HttpGet("GetAllCar")]
    public async Task<IActionResult> GetAllCars()
    {
        try
        {
            var cars = await _context.Cars.ToListAsync();

            return Ok(cars);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    // GET by ID
    [HttpGet("GetCarById/{id}")]
    public async Task<ActionResult> GetCarById(int id)
    {
        try
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound("Macchina non trovata");
            }

            return Ok(car);
        }catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    
    [HttpPost("InsertCar")]
    public async Task<ActionResult<Car>> InsertCar(Car car)
    {
        try
        {
            // check della targa
            var plateCheck = await _context.Cars.FirstOrDefaultAsync(c => c.LicensePlate == car.LicensePlate);

            if(plateCheck != null)
            {
                return BadRequest("Macchina gia registrata");
            }

            _context.Cars.Add(car);

            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione qui
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    // DELETE 
    [HttpDelete("DeleteCar")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        try
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound("Macchina non trovata");
            }

            _context.Cars.Remove(car);

            await _context.SaveChangesAsync();

            return Ok("Macchina eliminata");
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
        
    }
    
}