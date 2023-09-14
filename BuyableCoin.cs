using Binance.NetCore;
using Binance.NetCore.Entities;
using tradebot;

public class BuyableCoin
{
    private TradeBotDbContext? Context;
    public BuyableCoin(string apiKey, string apiSecretKey)
    {
        Context = new TradeBotDbContext();
    }

    public string Execute()
    {
        if (GetBuyableCoin("BitTorrent(New)"))
            return "BitTorrent(New)";
        else if (GetBuyableCoin("Pepe"))
            return "Pepe";
        else if (GetBuyableCoin("Shiba Inu"))
            return "Shiba Inu";
        return "";
    }

    public bool GetBuyableCoin(string CoinName)
    {
        List<decimal> lowPrice = new List<decimal>();
        List<decimal> highPrice = new List<decimal>();
        List<decimal> price = new List<decimal>();
        if (Context != null)
        {
            Context.Currencies.Where(x => x.name == "BitTorrent(New)").ToList().ForEach(x =>
                    {
                        if (x.price != null)
                        {
                            price.Add(Convert.ToDecimal(x.price));
                        }
                    });
                    decimal avg = 0;
                    price.ForEach(x =>
                    {
                        avg += x;
                    });
                    avg = avg / price.Count;
                    price.ForEach(x =>
                    {
                        if (x < avg)
                        {
                            lowPrice.Add(x);
                        }
                        else
                        {
                            highPrice.Add(x);
                        }
                    });
                    decimal lowAvg = 0;
                    lowPrice.ForEach(x =>
                    {
                        lowAvg += x;
                    });
                    lowAvg = lowAvg / lowPrice.Count;
                    decimal highAvg = 0;
                    highPrice.ForEach(x =>
                    {
                        highAvg += x;
                    });
                    highAvg = highAvg / highPrice.Count;
                    decimal rs = highAvg / lowAvg;
                    decimal rsi = 100 - (100 / (1 + rs));
                    if (rsi > 30)
                    {
                        Console.WriteLine(CoinName + " Alinabilir" + rsi);
                        return true;
                    }
        }
        Console.WriteLine("Alinabilir Coin Yok");
        return false;
    
    } 
}