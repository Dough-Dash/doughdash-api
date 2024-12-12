using doughdash_api.Models;
using Microsoft.EntityFrameworkCore;

namespace doughdash_api.Data;

public class DoughDashContext : DbContext
{
    public DoughDashContext(DbContextOptions<DoughDashContext> options) : base(options)
    {
    }

    public DbSet<Rider> Riders { get; set; }
    public DbSet<Pizzeria> Pizzerie { get; set; }
    public DbSet<AccessCode> AccessCodes { get; set; }
    public DbSet<Cliente> Clienti { get; set; }
    public DbSet<DettagliOrdine> DettagliOrdine { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<Ordine> Ordini { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DettagliOrdine>()
            .HasKey(d => new { d.IdOrdine, d.Prodotto });

        modelBuilder.Entity<Menu>().HasKey(m => new { m.IDProdotto });
        modelBuilder.Entity<Ordine>().HasKey(o => new { o.IDOrdine });
    }
}