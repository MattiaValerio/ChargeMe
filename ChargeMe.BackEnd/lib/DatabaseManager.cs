using ChargeMe.BackEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace ChargeMe.BackEnd.lib
{
    public class DatabaseManager
    {
        public static void MigrateDatabase(IApplicationBuilder app)
        {
            int Try = 0;
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();


            while (!context.Database.CanConnect() && Try <= 5)
            {
                Console.WriteLine("DB not ready...");
                Try += 1;
                Thread.Sleep(5000);
            }

            if (context.Database.CanConnect())
            {
                context.Database.Migrate();
            }

            Try = 0;
        }
    }
}
