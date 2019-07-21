using Stocks.API.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocks.API.Repositories
{
    public interface IStocksRepository
    {
        IEnumerable<Stock> GetStocks();

        Task<IEnumerable<Stock>> GetStocksAsync();

        Task<IEnumerable<Stock>> GetStocksAsync(string stockName);
    }
}
