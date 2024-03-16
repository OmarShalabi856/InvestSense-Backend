using AutoMapper;
using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Models;
using InvestSense_API.Services;
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
		public readonly IStockRepository _stockRepository;
		public StockController(ApplicationDbContext context,IMapper mapper,IStockRepository stockRepository)
		{ 
			_context = context;
			_mapper = mapper;
			_stockRepository = stockRepository;	
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var stocks = await _stockRepository.GetAllAsync();
			var stocksDTO = stocks.Select(s => _mapper.Map<StockDTO>(s)).ToList() ;
			return Ok(stocksDTO);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var stock = await _stockRepository.GetByIdAsync(id);
			if (stock == null)
			{
				return NotFound();
			}
			var stockDTO = _mapper.Map<StockDTO>(stock);
			return Ok(stockDTO);
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO createStockDTO )
		{
			var stockCreated = _mapper.Map<Stock>(createStockDTO);
			await _stockRepository.CreateAsync(stockCreated);
			return CreatedAtAction(nameof(GetById), new { stockCreated.Id }, createStockDTO);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStockRequestDTO)
		{
			var existingStock = await _stockRepository.GetByIdAsync(id);
			if(existingStock == null)
			{
				return NotFound();
			}

			await _stockRepository.UpdateAsync(id, updateStockRequestDTO);


			return Ok(_mapper.Map<StockDTO>(existingStock));
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var existingStock = await _stockRepository.GetByIdAsync(id);
			if (existingStock == null)
			{
				return NotFound();
			}

			await _stockRepository.DeleteAsync(id);


			return NoContent();
		}

	}
}
