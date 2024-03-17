using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Helpers;
using InvestSense_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestSense_API.Services
{
	public class StockRepository(ApplicationDbContext context) : Repository<Stock>(context), IStockRepository
	{

		public  async Task<List<Stock>> GetAllWithCommentsAsync(StockQueryObject stockQueryObject)
		{
			var stocks= context.Stock.Include(s=>s.Comments).AsQueryable();

			int skipNumber = (stockQueryObject.PageNumber - 1) * stockQueryObject.PageSize;

			if (!string.IsNullOrWhiteSpace(stockQueryObject.CompanyName))
			{
				stocks = stocks.Where(s => s.CompanyName.Contains(stockQueryObject.CompanyName));
			}

			if (!string.IsNullOrWhiteSpace(stockQueryObject.Symbol))
			{
				stocks = stocks.Where(s => s.Symbol.Contains(stockQueryObject.Symbol));
			}

			if (!string.IsNullOrWhiteSpace(stockQueryObject.SortBy))
			{
				if (stockQueryObject.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
				{
					stocks = stockQueryObject.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
				}
				else if (stockQueryObject.SortBy.Equals("Company Name",StringComparison.OrdinalIgnoreCase))
				{
					stocks = stockQueryObject.IsDescending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy(s => s.CompanyName);
				}
			}

			return await stocks.Skip(skipNumber).Take(stockQueryObject.PageSize).ToListAsync();
		}

		public async Task<bool> CheckStockExists(int stockId)
		{
			return await context.Stock.AnyAsync(s=>s.Id==stockId);
		}
	}
}
