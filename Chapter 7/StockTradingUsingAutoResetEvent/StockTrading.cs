using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StockTradingUsingAutoResetEvent
{
    public class StockTrading
    {
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public int currentStockPriceOfXYZ { get; set; } //Holds current stock price
        public int buyPriceofXYZ { get; set; } //Buy price of stocks 
        public bool StockPurchased { get; set; } //Flag that is set to true once order is successful

        public StockTrading(bool stockPurchased)
        {
            this.StockPurchased = stockPurchased;
        }

        public void PlaceOrder()
        {
            Console.WriteLine("Enter price at which you want to buy XYZ (minimum 1, maximum 5)");
            buyPriceofXYZ = Convert.ToInt32(Console.ReadLine());
            this.StockPurchased = false;
            autoResetEvent.WaitOne(); //Wait until receives signal from price validation
            Console.WriteLine($"Stock purchased at buy price of {buyPriceofXYZ}");
            Console.WriteLine("One stock order is completed, press enter to exit");
            this.StockPurchased = true;
            Console.ReadLine();
        }

        public void ValidatePrice()
        {
            if (this.buyPriceofXYZ == this.currentStockPriceOfXYZ)
            {
                Console.WriteLine($"Current stock price of {this.currentStockPriceOfXYZ} is matching with buy price of {this.buyPriceofXYZ}");
                autoResetEvent.Set(); //Signal first thread waiting in queue to execute     
            }
            else if (!this.StockPurchased)
            {
                Console.WriteLine($"Current stock price of {this.currentStockPriceOfXYZ} is not matching with buy price of {this.buyPriceofXYZ}");
            }
        }

    }
}
