using InvestSense_API.DTOs;
using InvestSense_API.Helpers;
using InvestSense_API.Models;

namespace InvestSense_API.Services
{
	public interface IStockRepository:IRepository<Stock>
	{
	
		public Task<List<Stock>> GetAllWithCommentsAsync(StockQueryObject stockQueryObject);
		public Task<bool> CheckStockExists(int stockId);
		public Task<Stock?> GetStockBySymbol(string symbol);	
	}
}
