using InvestSense_API.Extensions;
using InvestSense_API.Models;
using InvestSense_API.Repository;
using InvestSense_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvestSense_API.Controllers
{
	[Route("api/portfolio")]
	[ApiController]
	public class PortfolioController : ControllerBase
	{
		public readonly UserManager<AppUser> _userManager;
		public readonly IStockRepository _stockRepository;
		public readonly IPortfolioRepository _portfolioRepository;
		public readonly IFMP _FMPService;


		public PortfolioController(UserManager<AppUser> userManager,IStockRepository stockRepository,IPortfolioRepository portfolioRepository,IFMP FMPService)
		{
			_userManager = userManager;
			_stockRepository = stockRepository;
			_portfolioRepository = portfolioRepository;
			_FMPService = FMPService;	
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetUserPortfolio()
		{
			try
			{
				var userName = User.GetUserName();
				var appUser = await _userManager.FindByNameAsync(userName);
				if (appUser == null)
				{
					return NotFound("User Not Found!");
				}
				return Ok(await _portfolioRepository.GetUserPortfolio(appUser));
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);	
			}
			

		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddPortfolio([FromQuery] string symbol)
		{
			try
			{
				var userName = User.GetUserName();
				var appUser = await _userManager.FindByNameAsync(userName);
				if (appUser == null)
				{
					return NotFound("User Not Found!");
				}
				var stock = await _stockRepository.GetStockBySymbol(symbol);
				if (stock == null)
				{
					stock = await _FMPService.FindStockBySymbolAsync(symbol);
					if (stock == null)
					{
						return BadRequest("Stock Does Not Exist!");
					}
					await _stockRepository.CreateAsync(stock);
				}

				var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

				if (userPortfolio.Any(i => i.Symbol.ToLower().Equals(symbol.ToLower())))
				{
					return BadRequest("Cannot Add An Exisiting Stock To Portfolio");
				}

				var portfolioModel = new Portfolio
				{
					StockId=stock.Id,
					AppUserId=appUser.Id
				};

				var portfilioCreated = await _portfolioRepository.CreateAsync(portfolioModel);

				if (portfilioCreated == null)
				{
					return StatusCode(500, "Could Not Create Portfolio!");
				}

				return Ok(portfilioCreated);	

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}


		}

		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> DeletePortfolio(string symbol)
		{
			try
			{
				var userName = User.GetUserName();
				var appUser = await _userManager.FindByNameAsync(userName);
				if (appUser == null)
				{
					return NotFound("User Not Found!");
				}
				

				var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

				var filteredStock = userPortfolio.FirstOrDefault(s=>s.Symbol.ToLower().Equals(symbol.ToLower()));
				if (filteredStock==null)
				{
					return BadRequest("Stock Not Found In The Portfolio");
				}

				var portfolioDeleted = await _portfolioRepository.DeleteStockFromPortfolio(appUser,filteredStock);

				return Ok(portfolioDeleted);

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}


		}
	}
}
