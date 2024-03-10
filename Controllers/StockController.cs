using AutoMapper;
using InvestSense_API.Data;
using InvestSense_API.DTOs;
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
		public IActionResult GetById([FromRoute] int id, IMapper _mapper)
		{
			var stock = _context.Stock.Find(id);
			if (stock == null)
			{
				return NotFound();
			}
			var stockDTO = _mapper.Map<StockDTO>(stock);
			return Ok(stockDTO);
		}
	}
}
