using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
    }
}