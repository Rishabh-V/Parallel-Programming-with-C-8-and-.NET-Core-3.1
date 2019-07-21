using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stocks.API.Repositories;

namespace Stocks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockSynchronousController : ControllerBase
    {
        private IStocksRepository _stocksRepository;

        public StockSynchronousController(IStocksRepository stocksRepository)
        {
            this._stocksRepository = stocksRepository;
        }

        // GET: api/Stocks
        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _stocksRepository.GetStocks();
            return Ok(stocks);
        }

    }
}
