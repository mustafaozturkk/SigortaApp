using Microsoft.EntityFrameworkCore;

namespace SigortaApp.API.DataAccessLayer
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=1234;Server =localhost;Port=5432;Database=SigortaAppApiDb;");
            //optionsBuilder.UseNpgsql("User ID=root;Password=1234;Host=localhost;Port=5432;Database=SigortaAppDb;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
