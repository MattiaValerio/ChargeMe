using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChargeMe.Shared.Models;

public class ChargingStation
{
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ChargingStationStatus Status { get; set; }
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Car? ChargingCar { get; set; }
}

public enum ChargingStationStatus
{
    Available = 0,
    Occupied = 1,
    OutOfService = 2
}

public record GetChargingStation(
    int Id,
    string Name,
    ChargingStationStatus Status,
    string City,
    string Address,
    string State,
    string ZipCode,
    double Latitude,
    double Longitude,
    GetCarId? ChargingCar
    );

public record InsertChargingStation(
    string Name,
    ChargingStationStatus Status,
    string City,
    string Address,
    string State,
    string ZipCode,
    double Latitude,
    double Longitude,
    Car? ChargingCarId
    );
