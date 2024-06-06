namespace ChargeMe.Shared.Models;

public class Charging : Car
{
    public required ChargingStation Station { get; set; } 
    public CarStatus ChargingStatus { get; set; }
    public int ChargingPerc { get; set; }
}

public enum CarStatus
{
    Charging,
    charged,
    NotCharging
}
