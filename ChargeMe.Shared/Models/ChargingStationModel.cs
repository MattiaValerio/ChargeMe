using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChargeMe.Shared.Models;

public class ChargingStation
{
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public string Name { get; set; }
    public ChargingStationStatus Status { get; set; }
    public string City { get; set; } 
    public string Address { get; set; } 
    public string State { get; set; } 
    public string ZipCode { get; set; } 
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public ChargingStation(string name, ChargingStationStatus status, string city, string address, string state, string zipCode, double latitude, double longitude)
    {
        Name = name;
        Status = status;
        City = city;
        Address = address;
        State = state;
        ZipCode = zipCode;
        Latitude = latitude;
        Longitude = longitude;
    }
}

public enum ChargingStationStatus
{
    Available = 0,
    Occupied = 1,
    OutOfService = 2
}

