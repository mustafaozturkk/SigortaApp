using Microsoft.EntityFrameworkCore;

namespace SigortaApp.MinimalAPii;

public class DbCoreContext : DbContext
{
    public DbCoreContext(DbContextOptions<DbCoreContext> options) : base(options) { }
    public DbSet<Task> Task { get; set; }

}
