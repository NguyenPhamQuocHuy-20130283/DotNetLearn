using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DB;
using api.Dtos.Stock;
using api.Interfaces;
using api.models;
using api.Utils;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {

            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {

                if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
                else if (queryObject.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy(s => s.CompanyName);
                }
                else if (queryObject.SortBy.Equals("Purchase", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.Purchase) : stocks.OrderBy(s => s.Purchase);
                }
                else if (queryObject.SortBy.Equals("LastDiv", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.LastDiv) : stocks.OrderBy(s => s.LastDiv);
                }
                else if (queryObject.SortBy.Equals("Industy", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.Industy) : stocks.OrderBy(s => s.Industy);
                }
                else if (queryObject.SortBy.Equals("MarketCap", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.MarketCap) : stocks.OrderBy(s => s.MarketCap);
                }

            }
            var skip = (queryObject.PageNumber - 1) * queryObject.PageSize;
            stocks = stocks.Skip(skip).Take(queryObject.PageSize);
            return await stocks.ToListAsync();
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