using Microsoft.EntityFrameworkCore;

namespace DataPostgres
{
    class ContextPostgres : DbContext
    {

        public DbSet<Client> Clients { set; get; }
        public DbSet<Order> Orders { set; get; }


        public ContextPostgres()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ClientOrder;Username=postgres;Password=261094");
        }
    }
}
