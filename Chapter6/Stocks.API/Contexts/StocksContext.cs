using Microsoft.EntityFrameworkCore;
using Stocks.API.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocks.API.Contexts
{
    public class StocksContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }

        public StocksContext(DbContextOptions<StocksContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Stock>().HasData(
                new Stock()
                {
                    Id = 1,
                    StockName = "ABC",
                    Price = 10.1,
                    TradeDate = DateTime.Today.Date.AddDays(-10),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 2,
                    StockName = "ABC",
                    Price = 2.1,
                    TradeDate = DateTime.Today.Date.AddDays(-9),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 3,
                    StockName = "ABC",
                    Price = 5.1,
                    TradeDate = DateTime.Today.Date.AddDays(-8),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 4,
                    StockName = "ABC",
                    Price = 8.1,
                    TradeDate = DateTime.Today.Date.AddDays(-7),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 5,
                    StockName = "ABC",
                    Price = 1.1,
                    TradeDate = DateTime.Today.Date.AddDays(-6),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 6,
                    StockName = "ABC",
                    Price = 10.1,
                    TradeDate = DateTime.Today.Date.AddDays(-5),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 7,
                    StockName = "ABC",
                    Price = 2.1,
                    TradeDate = DateTime.Today.Date.AddDays(-4),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 8,
                    StockName = "ABC",
                    Price = 5.1,
                    TradeDate = DateTime.Today.Date.AddDays(-3),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 9,
                    StockName = "ABC",
                    Price = 8.1,
                    TradeDate = DateTime.Today.Date.AddDays(-2),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 10,
                    StockName = "ABC",
                    Price = 1.1,
                    TradeDate = DateTime.Today.Date.AddDays(-1),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 11,
                    StockName = "XYZ",
                    Price = 10.1,
                    TradeDate = DateTime.Today.Date.AddDays(-10),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 12,
                    StockName = "XYZ",
                    Price = 2.1,
                    TradeDate = DateTime.Today.Date.AddDays(-9),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 13,
                    StockName = "XYZ",
                    Price = 5.1,
                    TradeDate = DateTime.Today.Date.AddDays(-8),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 14,
                    StockName = "XYZ",
                    Price = 8.1,
                    TradeDate = DateTime.Today.Date.AddDays(-7),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 15,
                    StockName = "XYZ",
                    Price = 1.1,
                    TradeDate = DateTime.Today.Date.AddDays(-6),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 16,
                    StockName = "XYZ",
                    Price = 10.1,
                    TradeDate = DateTime.Today.Date.AddDays(-5),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 17,
                    StockName = "XYZ",
                    Price = 2.1,
                    TradeDate = DateTime.Today.Date.AddDays(-4),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 18,
                    StockName = "XYZ",
                    Price = 5.1,
                    TradeDate = DateTime.Today.Date.AddDays(-3),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 19,
                    StockName = "XYZ",
                    Price = 8.1,
                    TradeDate = DateTime.Today.Date.AddDays(-2),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 20,
                    StockName = "XYZ",
                    Price = 1.1,
                    TradeDate = DateTime.Today.Date.AddDays(-1),
                    Volume = 5000

                },
                new Stock(){
                Id = 21,
                    StockName = "ABC",
                    Price = 10.1,
                    TradeDate = DateTime.Today.Date.AddDays(-20),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 22,
                    StockName = "ABC",
                    Price = 2.1,
                    TradeDate = DateTime.Today.Date.AddDays(-19),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 23,
                    StockName = "ABC",
                    Price = 5.1,
                    TradeDate = DateTime.Today.Date.AddDays(-18),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 24,
                    StockName = "ABC",
                    Price = 8.1,
                    TradeDate = DateTime.Today.Date.AddDays(-17),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 25,
                    StockName = "ABC",
                    Price = 1.1,
                    TradeDate = DateTime.Today.Date.AddDays(-16),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 26,
                    StockName = "ABC",
                    Price = 10.1,
                    TradeDate = DateTime.Today.Date.AddDays(-15),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 27,
                    StockName = "ABC",
                    Price = 2.1,
                    TradeDate = DateTime.Today.Date.AddDays(-14),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 28,
                    StockName = "ABC",
                    Price = 5.1,
                    TradeDate = DateTime.Today.Date.AddDays(-13),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 29,
                    StockName = "ABC",
                    Price = 8.1,
                    TradeDate = DateTime.Today.Date.AddDays(-12),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 30,
                    StockName = "ABC",
                    Price = 1.1,
                    TradeDate = DateTime.Today.Date.AddDays(-11),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 31,
                    StockName = "XYZ",
                    Price = 10.1,
                    TradeDate = DateTime.Today.Date.AddDays(-20),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 32,
                    StockName = "XYZ",
                    Price = 2.1,
                    TradeDate = DateTime.Today.Date.AddDays(-19),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 33,
                    StockName = "XYZ",
                    Price = 5.1,
                    TradeDate = DateTime.Today.Date.AddDays(-18),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 34,
                    StockName = "XYZ",
                    Price = 8.1,
                    TradeDate = DateTime.Today.Date.AddDays(-17),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 35,
                    StockName = "XYZ",
                    Price = 1.1,
                    TradeDate = DateTime.Today.Date.AddDays(-16),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 36,
                    StockName = "XYZ",
                    Price = 10.1,
                    TradeDate = DateTime.Today.Date.AddDays(-15),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 37,
                    StockName = "XYZ",
                    Price = 2.1,
                    TradeDate = DateTime.Today.Date.AddDays(-14),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 38,
                    StockName = "XYZ",
                    Price = 5.1,
                    TradeDate = DateTime.Today.Date.AddDays(-13),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 39,
                    StockName = "XYZ",
                    Price = 8.1,
                    TradeDate = DateTime.Today.Date.AddDays(-12),
                    Volume = 5000

                },
                new Stock()
                {
                    Id = 40,
                    StockName = "XYZ",
                    Price = 1.1,
                    TradeDate = DateTime.Today.Date.AddDays(-11),
                    Volume = 5000

                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
