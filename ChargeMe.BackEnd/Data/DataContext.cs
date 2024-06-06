using ChargeMe.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChargeMe.BackEnd.Data;

public class DataContext : IdentityDbContext
{
    private DbContextOptions<DataContext> _options;
    private IConfiguration _conf;

    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
        _options = options;
        _conf = configuration; //leggiamo la string di connessione al DB
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_conf.GetConnectionString("DefaultConnection")); // ci colleghiamo al DB
    }

    public DbSet<ChargingStation> ChargingStations { get; set; }
    public DbSet<Charging> Chargings { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
