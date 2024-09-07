using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DB;
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
        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync();
        }
    }
}