using tradebot;

public class Program
{
    public static void Main(string[] args)
    {
        string apiKey = "";
        string apiSecretKey = "";
        Balance balance = new Balance(apiKey, apiSecretKey);
        while (true)
        {
            Console.WriteLine("Data Reading...");
            GetPrice.SavePrice();
            if (balance.GetUSDTBalance() >= 5)
            {
                Buy buy = new Buy(apiKey, apiSecretKey);
                BuyableCoin buyableCoin = new BuyableCoin(apiKey, apiSecretKey);
                buy.Execute(buyableCoin.Execute());
            }
            else
            {
                Console.WriteLine("Alim Icin Yetersiz Bakiye");
            }
            if (balance.WithAValueOf5USDT() == true)
            {
                Sell sell = new Sell(apiKey, apiSecretKey);
                SellableCoin sellableCoin = new SellableCoin(apiKey, apiSecretKey);
                sell.Execute(sellableCoin.Execute());
            }
            else
            {
                Console.WriteLine("Satis Icin Yetersiz Bakiye");
            }
            Thread.Sleep(360000);
        }
    }
}