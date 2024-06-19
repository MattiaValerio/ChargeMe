using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChargeMe.Shared.Models;

public class Car
{
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
}

public record GetCar(
    int Id, 
    string Brand,
    string Model,  
    string Year,
    string LicencePlate,
    string Dicriminator
    );

public record GetCarId(
    int Id
    );


public record InsertCar(
    string Brand,
    string Model,
    string Year,
    string LicensePlate,
    string Discriminator
    );
    