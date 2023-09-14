using Binance.NetCore;
using Binance.NetCore.Entities;

public class Buy
{
    string apiKey = "";
    string apiSecretKey = "";
    private BinanceApiClient? binanceApiClient = null;
    public Buy(string apiKey, string apiSecretKey)
    {
        binanceApiClient = new BinanceApiClient(apiKey, apiSecretKey);
        this.apiKey = apiKey;
        this.apiSecretKey = apiSecretKey;
    }
    public void Execute(String coin)
    {
        if (coin != "")
        {
            if (coin == "BitTorrent(New)")
                coin = "BTTUSDT";
            if (coin == "Pepe")
                coin = "PEPEUSDT";
            if (coin == "Shiba Inu")
                coin = "SHIBUSDT";
            
            if (binanceApiClient != null)
            {
                Tick[] tick = binanceApiClient.Get24HourStats(coin);
                try
                {
                    var tradeParams = new TradeParams
                    {
                        price = new Balance(apiKey, apiSecretKey).GetBalance(coin),
                        stopPrice = Convert.ToDecimal(tick[0].lastPrice),
                        quantity = new Balance(apiKey, apiSecretKey).GetBalance(coin),
                        side = Side.BUY.ToString(),
                        symbol = coin,
                        type = OrderType.STOP_LOSS_LIMIT.ToString()
                    };
                    binanceApiClient.PostTrade(tradeParams);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}