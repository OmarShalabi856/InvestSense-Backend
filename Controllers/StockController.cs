using AutoMapper;
using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult Get()
		{
			var stocks= _context.Stock.ToList();
			var stocksDTO = stocks.Select(s => _mapper.Map<StockDTO>(s)).ToList() ;
			return Ok(stocksDTO);
		}

		[HttpGet("{id}")]
		public IActionResult GetById([FromRoute] int id)
		{
			var stock = _context.Stock.Find(id);
			if (stock == null)
			{
				return NotFound();
			}
			var stockDTO = _mapper.Map<StockDTO>(stock);
			return Ok(stockDTO);
		}


		[HttpPost]
		public IActionResult Create([FromRoute] CreateStockRequestDTO createStockDTO )
		{
			var stockCreated = _mapper.Map<Stock>(createStockDTO);
			_context.Add(stockCreated);
			_context.SaveChanges();
			return CreatedAtAction(nameof(GetById), new { Id = stockCreated.Id }, createStockDTO);
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStockRequestDTO)
		{
			var existingStock = _context.Stock.Find(id);
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

			 _context.SaveChanges();


			return Ok(_mapper.Map<StockDTO>(existingStock));
		}

	}
}
