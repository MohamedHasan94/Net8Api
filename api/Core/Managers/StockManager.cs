using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core.Managers
{
    public class StockManager : Repository<Stock, ApplicationDbContext>
    {
        readonly ApplicationDbContext _context;
        public StockManager(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Stock?> GetByIdAsync(params object[] id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == (int)id[0]);
        }

        public override async Task<bool> Exists(params object[] id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == (int)id[0]);
        }

        public override async Task<bool> UpdateAsync(Stock updatedStock)
        {
            var stock = await _context.Stocks.FindAsync(updatedStock.Id);
            if (stock != null)
            {
                stock.Symbol = updatedStock.Symbol;
                stock.CompanyName = updatedStock.CompanyName;
                stock.Purchase = updatedStock.Purchase;
                stock.LastDiv = updatedStock.LastDiv;
                stock.Industry = updatedStock.Industry;
                stock.MarketCap = updatedStock.MarketCap;
                _context.Stocks.Update(stock);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}