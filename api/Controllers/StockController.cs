using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Core;
using api.Core.Managers;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // readonly ApplicationDbContext _dbContext;
        readonly IUnitOfWork _unitOfWork;
        public StockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var stocks = await _dbContext.Stocks.Select(s => s.ToStockDto()).ToListAsync();
            var stocks = await _unitOfWork.StockManager.GetAll().Select(s => s.ToStockDto()).ToListAsync();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // var stock = await _dbContext.Stocks.FindAsync(id);
            var stock = await _unitOfWork.StockManager.GetByIdAsync(id);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            var stock = stockDto.CreateDtoToStock();
            await _unitOfWork.StockManager.CreateAsync(stock);
            // await _dbContext.Stocks.AddAsync(stock);
            // await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateStockDto updatedStock)
        {
            // var stock = await _dbContext.Stocks.FindAsync(id);
            var stock = await _unitOfWork.StockManager.GetByIdAsync(id);
            if (stock != null)
            {
                stock.Symbol = updatedStock.Symbol;
                stock.CompanyName = updatedStock.CompanyName;
                stock.Purchase = updatedStock.Purchase;
                stock.LastDiv = updatedStock.LastDiv;
                stock.Industry = updatedStock.Industry;
                stock.MarketCap = updatedStock.MarketCap;
                await _unitOfWork.StockManager.UpdateAsync(stock);
                // await _dbContext.SaveChangesAsync();
                return Ok(stock.ToStockDto());
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            // var stock = await _dbContext.Stocks.FindAsync(id);
            var stock = await _unitOfWork.StockManager.GetByIdAsync(id);
            if (stock != null)
            {
                // _dbContext.Stocks.Remove(stock);
                // await _dbContext.SaveChangesAsync();
                await _unitOfWork.StockManager.DeleteAsync(stock);
                return NoContent();
            }
            return NotFound();
        }
    }
}