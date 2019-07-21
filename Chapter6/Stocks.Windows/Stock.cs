using System;

namespace Stocks.Windows
{
    public class Stock
    {
        public string StockName { get; set; }
        public int Volume { get; set; }
        public double Price { get; set; }
        public DateTime TradeDate { get; set; }
    }
}