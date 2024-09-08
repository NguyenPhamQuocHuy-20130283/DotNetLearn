using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DB;
using api.Dtos.Stock;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepo
    {
        private readonly DBContext _context;
        public StockRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);//remove dont have async
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<bool> StockExistsAsync(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequest stock)
        {
            var stockToUpdate = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockToUpdate == null)
            {
                return null;
            }
            stockToUpdate.Symbol = stock.Symbol;
            stockToUpdate.CompanyName = stock.CompanyName;
            stockToUpdate.Purchase = stock.Purchase;
            stockToUpdate.LastDiv = stock.LastDiv;
            stockToUpdate.Industy = stock.Industy;
            stockToUpdate.MarketCap = stock.MarketCap;
            await _context.SaveChangesAsync();
            return stockToUpdate;
        }
    }
}