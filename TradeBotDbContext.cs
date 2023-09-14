using Microsoft.EntityFrameworkCore;
using NoobsMuc.Coinmarketcap.Client;

namespace tradebot;

public class TradeBotDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=tradebot.db");
        base.OnConfiguring(optionsBuilder);
    }
    
    public DbSet<Crypto> Currencies { get; set; }
}