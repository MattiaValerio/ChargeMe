namespace ChargeMe.Shared.Models;

public class Charging : Car
{
    public required ChargingStation Station { get; set; } 
    public CarStatus ChargingStatus { get; set; }
    public int ChargingPerc { get; set; }
    public int ChargingPrice { get; set; }
    public double Voltage { get; set; }
    public TimeOnly LeastTime { get; set; }
    
}

public enum CarStatus
{
    Charging,
    charged,
    NotCharging
}


