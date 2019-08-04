using System;

namespace Stocks.Windows
{
    public class Stock
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public int Volume { get; set; }
        public double Price { get; set; }
        public DateTime TradeDate { get; set; }
    }
}