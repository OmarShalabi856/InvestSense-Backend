using AutoMapper;
using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Helpers;
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
		public async Task<IActionResult> GetAll([FromQuery] StockQueryObject stockQueryObject)
		{
			
			var stocks = await _stockRepository.GetAllWithCommentsAsync(stockQueryObject);
			var stocksDTO = stocks.Select(s => _mapper.Map<StockDTO>(s)).ToList() ;
			return Ok(stocksDTO);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
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
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var stockCreated = _mapper.Map<Stock>(createStockDTO);
			await _stockRepository.CreateAsync(stockCreated);
			return CreatedAtAction(nameof(GetById), new { stockCreated.Id }, createStockDTO);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStockRequestDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existingStock = await _stockRepository.GetByIdAsync(id);
			if(existingStock == null)
			{
				return NotFound();
			}
			var updatedStock = _mapper.Map<Stock>(updateStockRequestDTO);
			updatedStock.Id = id;
			await _stockRepository.UpdateAsync(id, updatedStock);


			return Ok(_mapper.Map<StockDTO>(existingStock));
		}


		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existingStock = await _stockRepository.GetByIdAsync(id);
			if (existingStock == null)
			{
				return NotFound();
			}

			await _stockRepository.DeleteAsync(id);


			return Ok();
		}

	}
}
