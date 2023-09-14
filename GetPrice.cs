using NoobsMuc.Coinmarketcap.Client;

namespace tradebot;

public class GetPrice
{
    public static void SavePrice()
    {
        ICoinmarketcapClient client = new CoinmarketcapClient("");
        var crypto = client.GetCurrencies();
        foreach (var c in crypto)
        {
            TradeBotDbContext context = new TradeBotDbContext();
            context.Currencies.Add(new Crypto
            {
                name = c.Name,
                date = DateTime.Now,
                price = c.Price
            });
            context.SaveChanges();
        }
    }
}