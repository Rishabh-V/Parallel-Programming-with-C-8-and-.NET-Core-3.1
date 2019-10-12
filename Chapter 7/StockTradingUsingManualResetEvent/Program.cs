using System;
using System.Threading;

namespace StockTradingUsingManualResetEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter buy price of stock XYZ");
            StockTrading stockTrading = new StockTrading(false);
            Console.WriteLine("Enter price at which you want to buy XYZ (minimum 1, maximum 5)");
            int buyPrice = Convert.ToInt32(Console.ReadLine());
            //Multiple threads to place 3 orders
            for (int i = 0; i < 3; i++)
            {
                Thread placeOrder = new Thread(() => stockTrading.PlaceOrder(i, buyPrice));
                Thread.Sleep(1000);
                placeOrder.Start();
            }


            Console.WriteLine("3 orders placed, press enter to start stock price matching!!");
            Console.ReadLine();
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
            Console.ReadLine();

            //Resetting ManualResetEvent to non-signaled state so that any subsequent orders are blocked (threads)
            //if this is not called call to .Wait method won't be blocked (Gate is open till Reset is called)
            stockTrading.manualResetEvent.Reset();
            Console.WriteLine("\nPlease enter buy price of stock XYZ");
            buyPrice = Convert.ToInt32(Console.ReadLine());
            //Multiple thread to place 2 more orders
            for (int i = 3; i < 5; i++)
            {
                Thread placeOrder = new Thread(() => stockTrading.PlaceOrder(i, buyPrice));
                Thread.Sleep(1000);
                placeOrder.Start();
            }
            Console.WriteLine("2 orders placed, press enter to start stock price matching!!");
            Console.ReadLine();
            //Thread that checks for current price and completes order
            validatePrice = new Thread(() =>
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

            Console.ReadLine();


        }
    }
}
