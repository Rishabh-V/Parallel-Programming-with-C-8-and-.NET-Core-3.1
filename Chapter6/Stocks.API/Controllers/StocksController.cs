using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            Thread.Sleep(10000);
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
    }
}
