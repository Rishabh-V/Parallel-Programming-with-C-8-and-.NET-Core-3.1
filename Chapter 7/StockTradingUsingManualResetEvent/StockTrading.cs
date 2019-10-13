using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StockTradingUsingManualResetEvent
{
    public class StockTrading
    {
        public ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);
        public int currentStockPriceOfXYZ { get; set; } //Holds current stock price
        public int buyPriceofXYZ { get; set; } //Buy price of stocks 
        public bool StockPurchased { get; set; } //Flag that is set to true once order is successful

        public StockTrading(bool stockPurchased)
        {
            this.StockPurchased = stockPurchased;
        }

        public void PlaceOrder(int threadId, int buyPrice)
        {
            buyPriceofXYZ = buyPrice;
            this.StockPurchased = false;
            manualResetEvent.Wait(); //Wait until receives signal from price validation
            Console.WriteLine($"Stock purchased at buy price of {buyPriceofXYZ}, Stock order {threadId} is completed");
            this.StockPurchased = true;
        }

        public void ValidatePrice()
        {
            if (this.buyPriceofXYZ == this.currentStockPriceOfXYZ)
            {
                Console.WriteLine($"Current stock price of {this.currentStockPriceOfXYZ} is matching with buy price of {this.buyPriceofXYZ}");
                manualResetEvent.Set(); //Signal first thread waiting in queue to execute     
            }
            else if (!this.StockPurchased)
            {
                Console.WriteLine($"Current stock price of {this.currentStockPriceOfXYZ} is not matching with buy price of {this.buyPriceofXYZ}");
            }
        }

    }
}
