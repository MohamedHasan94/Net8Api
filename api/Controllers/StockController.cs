using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Core;
using api.Core.Managers;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
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
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stocks = _unitOfWork.StockManager.GetAll();
            if (!string.IsNullOrEmpty(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.ToLower().Contains(query.Symbol.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.ToLower().Contains(query.CompanyName.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.Equals("symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
                else if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy( s => s.CompanyName);
                }
            }
            PaginatedList<StockDto> stocks1 = PaginatedList<StockDto>.Create(stocks.Select(s => s.ToStockDto()), query.PageIndex, query.PageSize);
            // var stocksList = await stocks.Select(s => s.ToStockDto()).ToListAsync();
            return Ok(stocks1);
        }

        [HttpGet("{id:int}")]
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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stock = stockDto.CreateDtoToStock();
            await _unitOfWork.StockManager.CreateAsync(stock);
            // await _dbContext.Stocks.AddAsync(stock);
            // await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateStockDto updatedStock)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if(await _unitOfWork.StockManager.UpdateAsync(updatedStock.CreateDtoToStock()))
            {
                return CreatedAtAction(nameof(Update), new {Id = id}, updatedStock.CreateDtoToStock());
            }
            
            return BadRequest("Update failed : please review stock data");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var stock = await _unitOfWork.StockManager.GetByIdAsync(id);
            if (stock != null)
            {
                await _unitOfWork.StockManager.DeleteAsync(stock);
                return NoContent();
            }
            return NotFound();
        }
    }
}