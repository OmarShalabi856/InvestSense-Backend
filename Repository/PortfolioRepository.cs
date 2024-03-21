using InvestSense_API.Data;
using InvestSense_API.Models;
using InvestSense_API.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestSense_API.Repository
{
	public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
	{
		private readonly ApplicationDbContext _context;

		public PortfolioRepository(ApplicationDbContext context):base(context)
		{
			_context = context;
		}

		public async Task<List<Stock>> GetUserPortfolio(AppUser user)
		{
			return await _context.Portfolio
				.Where(p => p.AppUserId == user.Id)
				.Select(s => new Stock
				{
					Id = s.Stock.Id,
					Symbol = s.Stock.Symbol,
					CompanyName = s.Stock.CompanyName,
					Price = s.Stock.Price,
					LastDividend = s.Stock.LastDividend,
					Industry = s.Stock.Industry,
					MarketCap = s.Stock.MarketCap
				})
				.ToListAsync();
		}
	}
}
