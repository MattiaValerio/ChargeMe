using ChargeMe.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChargeMe.BackEnd.Data;

public class DataContext : IdentityDbContext
{
    private IConfiguration _conf;

    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
        _conf = configuration; //leggiamo la string di connessione al DB
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_conf.GetConnectionString("DefaultConnection")); // ci colleghiamo al DB
    }

    public DbSet<ChargingStation> ChargingStations { get; set; }
    public DbSet<Charging> Chargings { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().HasData(
            new Car { Id = 1, Brand = "Tesla", Model = "Model S", LicensePlate = "AB123CD" },
            new Car { Id = 2, Brand = "Tesla", Model = "Model 3", LicensePlate = "EF456GH" },
            new Car { Id = 3, Brand = "Tesla", Model = "Model X", LicensePlate = "IJ789KL" },
            new Car { Id = 4, Brand = "Tesla", Model = "Model Y", LicensePlate = "MN012OP" }
            );

        base.OnModelCreating(modelBuilder);
    }
}
