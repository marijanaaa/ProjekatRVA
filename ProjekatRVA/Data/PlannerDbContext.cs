using Microsoft.EntityFrameworkCore;

using ProjekatRVA.Models;

namespace ProjekatRVA.Data
{
    public class PlannerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Planner> Planners { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<LoggedInUsers> LoggedInUsers { get; set; }

        public PlannerDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlannerDbContext).Assembly);
        }
    }
}
