using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ChargeMe.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChargeMe.BackEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChargingStationController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ChargingStationController(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }
    
    // GET
    [HttpGet("GetAllChargingStations")]
    public async Task<IActionResult> GetAllChargingStations()
    {
        var chargingStations = new List<GetChargingStation>();

        const string query = """
            SELECT cs."Id", cs."Name", cs."Status", cs."City", cs."Address", cs."State", cs."ZipCode", cs."Latitude", cs."Longitude", 
               c."Id" as "CarId", c."Brand", c."Model", c."Year", c."LicensePlate", c."Owner"
            FROM public."ChargingStations" cs
            LEFT JOIN public."Cars" c ON cs."ChargingCarId" = c."Id"
        """;

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = new NpgsqlCommand(query, connection);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            GetCarId? chargingCar = null;
            if (!reader.IsDBNull(reader.GetOrdinal("CarId")))
            {
                chargingCar = new GetCarId(
                    reader.GetInt32(reader.GetOrdinal("CarId"))
                    );
            }

            var station = new GetChargingStation(
                reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Name")),
                (ChargingStationStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                reader.GetString(reader.GetOrdinal("City")),
                reader.GetString(reader.GetOrdinal("Address")),
                reader.GetString(reader.GetOrdinal("State")),
                reader.GetString(reader.GetOrdinal("ZipCode")),
                reader.GetDouble(reader.GetOrdinal("Latitude")),
                reader.GetDouble(reader.GetOrdinal("Longitude")),
                chargingCar
            );

            chargingStations.Add(station);
        }

        return Ok(chargingStations);
    }



    
    
    
    // POST 
    // [HttpPost("InsertChargingStation")]
    // public async Task<IActionResult> InsertChargingStation([FromBody] InsertChargingStation chargingStation)
    // {
    //     const string query = """
    //         INSERT INTO public."ChargingStations" 
    //         ("Name", "Status", "City", "Address", "State", "ZipCode", "Latitude", "Longitude", "ChargingCarId") 
    //         VALUES 
    //         (@Name, @Status, @City, @Address, @State, @ZipCode, @Latitude, @Longitude, @ChargingCarId)
    //         RETURNING "Id";
    //     """;
    //
    //     using var connection = new NpgsqlConnection(_connectionString);
    //     await connection.OpenAsync();
    //
    //     using var cmd = new NpgsqlCommand(query, connection);
    //     cmd.Parameters.AddWithValue("Name", chargingStation.Name);
    //     cmd.Parameters.AddWithValue("Status", chargingStation.Status.ToString());
    //     cmd.Parameters.AddWithValue("City", chargingStation.City);
    //     cmd.Parameters.AddWithValue("Address", chargingStation.Address);
    //     cmd.Parameters.AddWithValue("State", chargingStation.State);
    //     cmd.Parameters.AddWithValue("ZipCode", chargingStation.ZipCode);
    //     cmd.Parameters.AddWithValue("Latitude", chargingStation.Latitude);
    //     cmd.Parameters.AddWithValue("Longitude", chargingStation.Longitude);
    //     cmd.Parameters.AddWithValue("ChargingCarId", chargingStation.ChargingCarId.HasValue ? (object)chargingStation.ChargingCarId.Value : DBNull.Value);
    //
    //     var newId = (int)await cmd.ExecuteScalarAsync();
    //
    //     //return CreatedAtAction(nameof(GetChargingStationById), new { id = newId }, new { id = newId });
    //     return Ok();
    //}

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetChargingStationById(int id)
    // {
    //     const string query = """
    //         SELECT "Id", "Name", "Status", "City", "Address", "State", "ZipCode", "Latitude", "Longitude", "ChargingCarId"
    //         FROM public."ChargingStations"
    //         WHERE "Id" = @Id
    //     """;
    //
    //     using var connection = new NpgsqlConnection(_connectionString);
    //     await connection.OpenAsync();
    //
    //     using var cmd = new NpgsqlCommand(query, connection);
    //     cmd.Parameters.AddWithValue("Id", id);
    //
    //     using var reader = await cmd.ExecuteReaderAsync();
    //
    //     if (await reader.ReadAsync())
    //     {
    //         var station = new GetChargingStation(
    //             reader.GetInt32(reader.GetOrdinal("Id")),
    //             reader.GetString(reader.GetOrdinal("Name")),
    //             Enum.Parse<ChargingStationStatus>(reader.GetString(reader.GetOrdinal("Status")), true),
    //             reader.GetString(reader.GetOrdinal("City")),
    //             reader.GetString(reader.GetOrdinal("Address")),
    //             reader.GetString(reader.GetOrdinal("State")),
    //             reader.GetString(reader.GetOrdinal("ZipCode")),
    //             reader.GetDouble(reader.GetOrdinal("Latitude")),
    //             reader.GetDouble(reader.GetOrdinal("Longitude")),
    //             reader.IsDBNull(reader.GetOrdinal("ChargingCarId")) ? null : new Car { Id = reader.GetInt32(reader.GetOrdinal("ChargingCarId")) }
    //         );
    //
    //         return Ok(station);
    //     }
    //
    //     return NotFound();
    // }

}