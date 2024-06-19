using ChargeMe.BackEnd.Migrations;
using ChargeMe.BackEnd.Services.CarServices;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ChargeMe.Shared.Models;

namespace ChargeMe.BackEnd.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public CarController(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }
    
    // GET
    [HttpGet("GetAllCar")]
    public async Task<IActionResult> GetAllCars()
    {
        List<GetCar> getcars = new List<GetCar>();

        const string query = """
                                SELECT "Id", "Brand", "Model", "Year", "LicensePlate", "Discriminator" 
                                FROM public."Cars"
                             """;
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var cmd = new NpgsqlCommand(query, connection);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var car = new GetCar(
                reader.GetInt32(reader.GetOrdinal("Id")), // Ensure the case matches exactly
                reader.GetString(reader.GetOrdinal("Brand")),
                reader.GetString(reader.GetOrdinal("Model")),
                reader.GetString(reader.GetOrdinal("Year")),
                reader.GetString(reader.GetOrdinal("LicensePlate")), // Corrected to match the SQL query
                reader.GetString(reader.GetOrdinal("Discriminator"))
            );
        
            getcars.Add(car);
        }

        return Ok(getcars);
    }
    
    // GET by ID
    [HttpGet("GetCarById/{id}")]
    public async Task<IActionResult> GetCarById(int id)
    {
        const string query = """
            SELECT "Id", "Brand", "Model", "Year", "LicensePlate", "Owner"
            FROM public."Cars"
            WHERE "Id" = @Id
        """;

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);

        await using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var car = new Car
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Brand = reader.GetString(reader.GetOrdinal("Brand")),
                Model = reader.GetString(reader.GetOrdinal("Model")),
                Year = reader.GetString(reader.GetOrdinal("Year")),
                LicensePlate = reader.GetString(reader.GetOrdinal("LicensePlate")),
                Owner = reader.GetGuid(reader.GetOrdinal("Owner"))
            };

            return Ok(car);
        }

        return NotFound(); // Return 404 if the car is not found
    }
    
    
    // POST 
    [HttpPost("InsertCar")]
    public async Task<IActionResult> InsertCar([FromBody] InsertCar car)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            // Esegui le operazioni con il database qui
            var cmd = new NpgsqlCommand("""
                            INSERT INTO public."Cars" ("Brand", "Model", "Year", "LicensePlate", "Discriminator") 
                            VALUES (@brand, @model, @year, @licenseplate, @discriminator)
                        """, connection);
            
            cmd.Parameters.AddWithValue("brand", car.Brand);
            cmd.Parameters.AddWithValue("model", car.Model);
            cmd.Parameters.AddWithValue("year", car.Year);
            cmd.Parameters.AddWithValue("licenseplate", car.LicensePlate);
            cmd.Parameters.AddWithValue("discriminator", car.Discriminator);
            
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione qui
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        return Ok();
    }
    
    // DELETE 
    [HttpDelete("DeleteCar")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        const string query = """
                                DELETE FROM public."Cars" 
                                WHERE "Id" = @Id
                             """;

        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("Id", id);

        int affectedRows = await cmd.ExecuteNonQueryAsync();

        if (affectedRows > 0)
        {
            return Ok(new { message = "Car deleted successfully." });
        }
        else
        {
            return NotFound(new { message = "Car not found." });
        }
    }
    
}