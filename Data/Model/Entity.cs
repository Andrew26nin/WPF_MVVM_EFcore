using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
   public class Entity : DbContext
    {

        public Entity()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MARK\BD;Database=ClientOrder;Trusted_Connection=True;");
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
