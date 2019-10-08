using System;
using System.Threading;

namespace StockTradingUsingAutoResetEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter buy price of stock XYZ");
            StockTrading stockTrading = new StockTrading(false);

            //Thread to place order
            Thread placeOrder = new Thread(stockTrading.PlaceOrder);
            placeOrder.Start();

            //Thread that checks for current price and completes order
            Thread validatePrice = new Thread(() =>
            {
                Random randomCurrentPriceofStock = new Random();
                while (!stockTrading.StockPurchased)
                {
                    stockTrading.currentStockPriceOfXYZ = randomCurrentPriceofStock.Next(1, 5);
                    stockTrading.ValidatePrice();
                    Thread.Sleep(1000); // Wait for input before execuiting next iteration or else screen will overflow
                }
            });
            validatePrice.Start();

        }
    }
}
