using Binance.NetCore;
using Binance.NetCore.Entities;

public class Balance
{

    BinanceApiClient? binanceApiClient = null;

    public Balance(string apiKey, string apiSecretKey)
    {
        binanceApiClient = new BinanceApiClient(apiKey, apiSecretKey);
    }

    public decimal GetUSDTBalance()
    {
        decimal usdtBalance = 0;
        if (binanceApiClient != null)
        {
            Account balance = binanceApiClient.GetBalance();
            if (balance != null)
            {
                balance.balances.ForEach(balance =>
            {
                if (balance.asset == "USDT")
                {
                    usdtBalance = balance.free;
                }
            });
            }
        }
        return usdtBalance;
    }

    public bool WithAValueOf5USDT()
    {
        bool status = false;
        decimal coinBalance = 0;
        if (binanceApiClient != null)
        {
            Account balance = binanceApiClient.GetBalance();
            if (balance != null)
            {
                balance.balances.ForEach(balance =>
                {
                    if (balance.asset == "SPELLUSDT")
                    {
                        Tick[] tick = binanceApiClient.Get24HourStats("SPELLUSDT");
                        coinBalance = balance.free / Convert.ToDecimal(tick[0].lastPrice);
                        if (coinBalance > 5)
                        {
                            status = true;
                        }
                    }
                });
            }
        }
        return status;
    }

    public decimal GetBalance(string coin)
    {
        decimal coinBalance = 0;
        if (binanceApiClient != null)
        {
            binanceApiClient.GetBalance().balances.ForEach(balance =>
            {
                if (balance.asset == coin)
                {
                    coinBalance = balance.free;
                }
            });
        }
        return coinBalance;
    }
}