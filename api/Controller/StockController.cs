using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DB;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly DBContext _dbContext;
        private readonly IStockRepo _stockRepo;
        public StockController(DBContext dbContext, IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _dbContext.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequest stockDto)
        {
            var stock = stockDto.ToStockFromCreateStockRequest();
            await _dbContext.Stocks.AddAsync(stock);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequest stockDto)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industy = stockDto.Industy;
            stock.MarketCap = stockDto.MarketCap;
            await _dbContext.SaveChangesAsync();
            return Ok(stock.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            _dbContext.Stocks.Remove(stock);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}