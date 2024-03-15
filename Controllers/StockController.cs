using AutoMapper;
using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestSense_API.Controllers
{
	[Route("api/stock")]
	[ApiController]
	public class StockController : ControllerBase
	{
		public readonly ApplicationDbContext _context;
		public readonly IMapper _mapper;
		public StockController(ApplicationDbContext context,IMapper mapper)
		{ 
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var stocks = await _context.Stock.ToListAsync();
			var stocksDTO = stocks.Select(s => _mapper.Map<StockDTO>(s)).ToList() ;
			return Ok(stocksDTO);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var stock = await _context.Stock.FindAsync(id);
			if (stock == null)
			{
				return NotFound();
			}
			var stockDTO = _mapper.Map<StockDTO>(stock);
			return Ok(stockDTO);
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromRoute] CreateStockRequestDTO createStockDTO )
		{
			var stockCreated = _mapper.Map<Stock>(createStockDTO);
			await _context.AddAsync(stockCreated);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new { stockCreated.Id }, createStockDTO);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStockRequestDTO)
		{
			var existingStock = await _context.Stock.FindAsync(id);
			if(existingStock == null)
			{
				return NotFound();
			}

			existingStock.Symbol = updateStockRequestDTO.Symbol;
			existingStock.CompanyName = updateStockRequestDTO.CompanyName;
			existingStock.Price = updateStockRequestDTO.Price;
			existingStock.LastDividend = updateStockRequestDTO.LastDividend;
			existingStock.Industry = updateStockRequestDTO.Industry;
			existingStock.MarketCap = updateStockRequestDTO.MarketCap;

			await _context.SaveChangesAsync();


			return Ok(_mapper.Map<StockDTO>(existingStock));
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var existingStock = await _context.Stock.FindAsync(id);
			if (existingStock == null)
			{
				return NotFound();
			}

			_context.Remove(existingStock);

			await _context.SaveChangesAsync();


			return NoContent();
		}

	}
}
