using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Stocks.API.Models.DAO
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public string StockName { get; set; }
        public int Volume { get; set; }
        public double Price { get; set; }
        public DateTime TradeDate { get; set; }
    }
}
