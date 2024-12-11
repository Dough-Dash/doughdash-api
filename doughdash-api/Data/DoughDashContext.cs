using doughdash_api.Models;
using Microsoft.EntityFrameworkCore;

namespace doughdash_api.Data
{
    public class DoughDashContext : DbContext
    {
        public DoughDashContext(DbContextOptions<DoughDashContext> options) : base(options)
        {
        }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Pizzeria> Pizzerie { get; set; }

    }
}
