using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stocks.API.Contexts;
using Stocks.API.Models.DAO;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;

namespace Stocks.API.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        StocksContext _context;

        public StocksRepository(StocksContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            _context.Database.EnsureCreated();
        }
        public IEnumerable<Stock> GetStocks()
        {
            return _context.Stocks.ToList();
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync(string stockName)
        {
            return await _context.Stocks.Where(t => t.StockName == stockName).ToListAsync();
        }

        public async Task AddStocksAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            _context.SaveChanges();
        }
    }
}
