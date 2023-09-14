using Binance.NetCore;
using Binance.NetCore.Entities;
using tradebot;

public class SellableCoin
{
    private TradeBotDbContext? Context;
    public SellableCoin(string apiKey, string apiSecretKey)
    {
        Context = new TradeBotDbContext();
    }
    public string Execute()
    {
        if (GetSellableCoin("BitTorrent(New)"))
            return "BitTorrent(New)";
        else if (GetSellableCoin("Pepe"))
            return "Pepe";
        else if (GetSellableCoin("Shiba Inu"))
            return "Shiba Inu";
        return "";
    }
    
    public bool GetSellableCoin(String CoinName)
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
                    if (rsi > 40)
                    {
                        Console.WriteLine(CoinName + " satilabilir" + rsi);
                        return true;
                    }
        }
        Console.WriteLine("Satilabilir Coin Yok");
        return false;
    }
}