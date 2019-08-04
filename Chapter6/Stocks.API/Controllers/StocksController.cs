using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stocks.API.Models.DAO;
using Stocks.API.Repositories;

namespace Stocks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {

        private IStocksRepository _stocksRepository;

        public StocksController(IStocksRepository stocksRepository)
        {
            this._stocksRepository = stocksRepository;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            await Task.Delay(5000);  // So that we can see delay in UI
            var stocks = await _stocksRepository.GetStocksAsync();
            return Ok(stocks);
        }

        // GET: api/Stocks/ABC
        [HttpGet("{stockName}", Name = "Get")]
        public async Task<IActionResult> GetStocksByName(string stockName)
        {
            var stocks = await _stocksRepository.GetStocksAsync(stockName);
            if (stocks.Any())
            {
                return Ok(stocks);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Stocks
        [HttpPost]
        public async Task<IActionResult> AddStock(Stock stock)
        {
            await Task.Delay(5000);  // So that we can see delay in UI
            if (stock == null)
            {
                return BadRequest();
            }
            else
            {
                await _stocksRepository.AddStocksAsync(stock);
                return Created($"https://localhost:44394/api/stock/{stock.StockName}", stock);
            }
        }
    }
}
